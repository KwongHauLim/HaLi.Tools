===== HaLi.Tools.Hashcode ====</br>
To calculate hash of binary, string, integer or from stream....


### Super easy use

    using HaLi.Tools.Hashcode;
      
      string hash;
      // Calc hash from string
      hash = Hash.GetHash("I am a message");
      // Calc hash from int
      hash = Hash.GetHash(1234567890);
      // or Calc from binary
      hash = Hash.GetHash(System.IO.File.ReadAllBytes("file.txt"));
      // or Calc from stream
      hash = Hash.GetHash(new FileStream("file.jpg", FileMode.Open));

\* Basic use just like that, default output sha1 hash


### Config
 Default is SHA256, but you can change to MD5 or other hash algorithm</br>
 Use Sha1 

    Hash.Share.Algorithm = new HaLi.Tools.Hashcode.SHA1
    {
        Encoding = System.Text.Encoding.UTF8,
	    UpperCase = true
    };

 Use MD5

    Hash.Share.Algorithm = new HaLi.Tools.Hashcode.MD5
    {
        Encoding = System.Text.Encoding.UTF8,
	    UpperCase = true
    };

### Other...
Base64... Actually it is SHA1</br>
It convert every 4 bytes into base64 and join into 1 string</br>
SHA1 hash is 20 bytes, should it will output 4 group of base64</br>

Covert int(4 bytes) with method below</br>
e.g. int : 1234567890</br>
bits like this 0100 1001 1001 0110 0000 0010 1101 0010</br>
It will change to other bytes[]</br>
1: 1101 0010</br>
2: 0010 1101</br>
3: 0000 0010</br>
4: 0110 0000</br>
5: 1001 0110</br>
6: 1001 1001</br>
7: 0100 1001</br>
8: 0010 0100</br>

> Did you see every byte have 4 bits is duplicated?</br>
> It used to check validation before check hash</br>

Cipher... Actually it is also sha1 inside</br>
It convert every 5 bits into a character from a 32 characters string</br>

>In fact, I am using function to convert int to 8 chars string.</br>
>I use it in server somewhere need a key or one-time password, and just generate a random integer store in database, but code pass to user always 8 chars string.</br>

### Custom?
Just Implement the interface IHashCalc </br>
    Hash.Share.Algorithm = new YourHashClass();

