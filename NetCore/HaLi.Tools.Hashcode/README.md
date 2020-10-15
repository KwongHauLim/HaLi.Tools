<p>===== HaLi.Tools.Hashcode ====</p>
<p>To calculate hash of binary, string, or from stream....</p>


<h3>Super easy use</h3>
<pre>
<code>
using HaLi.Tools.Hashcode;
  
  string hash;
  // Calc hash from string
  hash = Hash.GetHash("I am a message");
  // or Calc from binary
  hash = Hash.GetHash(System.IO.File.ReadAllBytes("file.txt"));
  // or Calc from stream
  hash = Hash.GetHash(new FileStream("file.jpg", FileMode.Open));
</code>
</pre>
<p>* Basic use just like that, default output sha1 hash</p>


<h3>Config</h3>
<p>
  Use Sha1 (Default)</br>
  <code>Hash.Share.Algorithm = new HaLi.Tools.Hashcode.SHA1();</code>
  </p>
  
<p>
  Use MD5</br>
  <code>Hash.Share.Algorithm = new HaLi.Tools.Hashcode.MD5();</code>
  </p>
