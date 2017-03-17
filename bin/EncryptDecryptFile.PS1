#################
# Powershell Allows The Loading of .NET Assemblies
# Load the Security assembly to use with this script 
#################
[Reflection.Assembly]::LoadWithPartialName("System.Security")

#################
# This function is to Encrypt A String.
# $string is the string to encrypt, $passphrase is a second security "password" that has to be passed to decrypt.
# $salt is used during the generation of the crypto password to prevent password guessing.
# $init is used to compute the crypto hash -- a checksum of the encryption
#################
function Encrypt-File($path, $Passphrase, $salt="SaltCrypto", $init="IV_Password", $outputPath)
{
    # Create a COM Object for RijndaelManaged Cryptography
	$r = new-Object System.Security.Cryptography.RijndaelManaged
	# Convert the Passphrase to UTF8 Bytes
	$pass = [Text.Encoding]::UTF8.GetBytes($Passphrase)
	# Convert the Salt to UTF Bytes
	$salt = [Text.Encoding]::UTF8.GetBytes($salt)

	# Create the Encryption Key using the passphrase, salt and SHA1 algorithm at 256 bits
	$r.Key = (new-Object Security.Cryptography.PasswordDeriveBytes $pass, $salt, "SHA1", 5).GetBytes(32) #256/8
	# Create the Intersecting Vector Cryptology Hash with the init
	$r.IV = (new-Object Security.Cryptography.SHA1Managed).ComputeHash( [Text.Encoding]::UTF8.GetBytes($init) )[0..15]
	
	# Starts the New Encryption using the Key and IV   
	$c = $r.CreateEncryptor()
	# Creates a MemoryStream to do the encryption in
	$ms = new-Object IO.MemoryStream
	# Creates the new Cryptology Stream --> Outputs to $MS or Memory Stream
	$cs = new-Object Security.Cryptography.CryptoStream $ms,$c,"Write"
	# Starts the new Cryptology Stream
	$fs = [IO.File]::OpenRead($path)
    $fs.CopyTo($cs);
    $fs.Close();
    # Stops the Cryptology Stream
	$cs.Close()
	# Stops writing to Memory
	$ms.Close()
	# Clears the IV and HASH from memory to prevent memory read attacks
	$r.Clear()
	# Takes the MemoryStream and puts it to an array
	[byte[]]$result = $ms.ToArray()

    $os = [IO.File]::OpenWrite($outputPath);
    $os.Write($result,0,$result.Length);
    $os.Close();
}

function Decrypt-File($path, $Passphrase, $salt="SaltCrypto", $init="IV_Password", $OutputPath)
{
	# Create a COM Object for RijndaelManaged Cryptography
	$r = new-Object System.Security.Cryptography.RijndaelManaged
	# Convert the Passphrase to UTF8 Bytes
	$pass = [Text.Encoding]::UTF8.GetBytes($Passphrase)
	# Convert the Salt to UTF Bytes
	$salt = [Text.Encoding]::UTF8.GetBytes($salt)

	# Create the Encryption Key using the passphrase, salt and SHA1 algorithm at 256 bits
	$r.Key = (new-Object Security.Cryptography.PasswordDeriveBytes $pass, $salt, "SHA1", 5).GetBytes(32) #256/8
	# Create the Intersecting Vector Cryptology Hash with the init
	$r.IV = (new-Object Security.Cryptography.SHA1Managed).ComputeHash( [Text.Encoding]::UTF8.GetBytes($init) )[0..15]

    $fs = [IO.File]::OpenRead($path)
    
	# Create a new Decryptor
	$d = $r.CreateDecryptor()
	# Create a New memory stream with the encrypted value.
	
	# Read the new memory stream and read it in the cryptology stream
	$cs = new-Object Security.Cryptography.CryptoStream $fs,$d,"Read"
	# Read the new decrypted stream

	# Return from the function the stream
	
    $os = [IO.File]::OpenWrite($outputPath);
    $cs.CopyTo($os);

    $os.Close();
    # Stops the crypology stream
	$cs.Close()
	
	# Stops the memory stream
	$fs.Close()
	# Clears the RijndaelManaged Cryptology IV and Key
	$r.Clear()
}

Decrypt-File "C:\projects\jmespath-net\src\jmespath.net.signed\jmespath.net.snk.crypted" -Passphrase $env:SNK_PASSPHRASE -salt $env:SNK_SALT -init $env:SNK_INIT  -outputPath "C:\projects\jmespath-net\src\jmespath.net.signed\jmespath.net.snk"