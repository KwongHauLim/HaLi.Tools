=== HaLi.Tools.Encryption ===</br>
Encrypt string 

### Super easy use
    using HaLi.Tools.Encryption;
    
    string msg = "I am a message...";
    string secret = Crypto.Encrypt(msg);
    msg = Crypto.Decrypt(secret);
    
### Config
Use AES (Default)

    // Generate AES key & IV
    string key;
    string iv;
    // there have out byte[] version, but string seems more convenience
    // !!! this is sample, MUST not generate everytime
    // use record KEY & IV for decrypt
    AES.GenerateKey(out key, out iv);
    
    // Set use AES
    Crypto.Share.Algorithm = new AES
    {
        Secret = new AES.CryptoBook(key, iv)
    };
    
Use Cipher</br>
This is very simple convertion, use a table which map a byte value change to another byte value

    // Generate bytes mapping table
    string key;
     // !!! this is sample, MUST not generate everytime
    // use record key
    Cipher.GenerateKey(out key);
    
    // Set to use Cipher
    Crypto.Share.Algorithm = new Cipher
    {
        Secret = new Cipher.CryptoBook(key)
    };
    
Use GZip</br>
Actually... it is compress function, not cryption XD

    // Set to use Cipher
    Crypto.Share.Algorithm = new GZip();
