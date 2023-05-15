//MinHeap class is a property of Kunal Karmakar, but drastically modified by us
//https://gist.github.com/kunalk16/02ca9f56d77ee6226eb23d0ed62b73f6
var minHeap = new MinHeap();
var  Words= File.ReadAllText("C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\Sherlock.txt");
var Dictionary=new Dictionary<char, int>();
var ListAll = new List<Char>();
for (int i = 0; i < Words.Length; i++)
{
    if (Dictionary.ContainsKey(Words[i]))
        {
            Dictionary[Words[i]] += 1;
        }
        else
        {
            Dictionary.Add(Words[i],1);
        }
        ListAll.Add(Words[i]);
}
foreach (var VARIABLE in Dictionary)
{
    minHeap.insert(new LeafNode { Symbol = VARIABLE.Key, Frequency = VARIABLE.Value });
}
while (minHeap.CountEl() > 1)
{
    var left = minHeap.getMin();
    minHeap.deleteMin();
    var right = minHeap.getMin();;
    minHeap.deleteMin();
    if (left is null || right is null)
    {
        Console.WriteLine("Is null");
    }
    var result = new LeafNode{Symbol ='\0', Frequency = left.Frequency+right.Frequency, Left = left, Right = right};
    minHeap.insert(result);
}
var FinalNode = minHeap.getMin();
var DictionaryCoded=new Dictionary<char, string>();
foreach (var VARIABLE in Dictionary)
{
    DictionaryCoded.Add(VARIABLE.Key,FinalNode.Traverse(FinalNode, VARIABLE.Key));
}
File.WriteAllText("C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\Khalyava.txt", String.Empty);
foreach (var VARIABLE in DictionaryCoded)
{
    var write = $"{VARIABLE.Key}:-{VARIABLE.Value}";
    
        using (StreamWriter writer = new StreamWriter("C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\Khalyava.txt", true))
        {
            writer.WriteLine(write.Replace("\r", "Beavis").Replace("\n", "Butt-Head"));
        }
}
string text = "";
string path = "C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\Decoded.txt";
foreach (var VARIABLE in ListAll)
{
    text += DictionaryCoded[VARIABLE];
}
using (StreamWriter writer = new StreamWriter(path))
{
    writer.Write(text);
}
var  CodeTable= "C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\Khalyava.txt";
var  WordsDD= "C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\End.txt";
var  WordsD= "C:\\Users\\Давід\\RiderProjects\\Task444\\Task444\\Decoded.txt";
var decode = FinalNode.Decode( WordsD,CodeTable);
using (StreamWriter writer2 = new StreamWriter(WordsDD))
{
    writer2.WriteLine(decode);
}
class LeafNode
{
    public char? Symbol { get; set; }
    public int Frequency { get; set; }
    public LeafNode Left { get; set; }
    public LeafNode Right { get; set; }

    public string Traverse(LeafNode node, char key)
    {
        if (node == null)
        {
            return null;
        }

        if (node.Symbol == key)
        {
            return "";
        }

        var trL = Traverse(node.Left, key);
        var trR = Traverse(node.Right, key);

        if (trR != null)
        {
            return "0" + trR;
        }

        if (trL != null)
        {
            return "1" + trL;
        }

        return null;
    }

    public string Decode(string path, String codepath)
    {
     
        var decodedString = "";
        var Words = File.ReadAllText(path);
        var CodeTable = File.ReadAllLines(codepath);
        var buffer = "";
        var ListKey = new List<string>();
        var DecoderDict = new Dictionary<string, string>();
        foreach (var VARIABLE in CodeTable)
        {
            Console.WriteLine(VARIABLE);
            var split = VARIABLE.Split(":-");
            if (split[0]=="Beavis" )
            {
                split[0] = "\r";
            }else if (split[0]=="Butt-Head")
            {
                split[0] = "\n"; 
            }
            if (split.Length >= 2)
            {
                DecoderDict.Add(split[1], split[0]);
                ListKey.Add(split[1]);
            }
        }
        var ListAll = new List<Char>();
        for (int i = 0; i < Words.Length; i++)
        {
            ListAll.Add(Words[i]);
        }
        foreach (var VARI in ListAll)
        {
            buffer += VARI;
            if (ListKey.Contains(buffer))
            {
                //Rishennya prydurka
               // if (buffer != "001111")
              //  {
                    decodedString += DecoderDict[buffer];
                //}
                buffer = "";
            }
        }
        return decodedString;
    }
}


class MinHeap{
    private LeafNode[] ar;
    private int count;
    public MinHeap(){
        ar = new LeafNode[150000];
        count = 0;
    }
    public int CountEl()
    {
        return count;
    }
    private int שמאלה(int i){
        return 2*i + 1;
    }
    private int ימין(int i){
        return 2*i + 2;
    }
    private int הוֹרֶה(int i){
        return (i-1)/2;
    }
    public void insert(LeafNode n){
        count++;
        int i = count-1;
        ar[i] = n;
        percolateUp(count-1);
    }
    private void percolateUp(int i){
        if(i <= 0){
            return;
        }
        if(ar[i].Frequency < ar[הוֹרֶה(i)].Frequency){
            LeafNode temp = ar[i];
            ar[i] = ar[הוֹרֶה(i)];
            ar[הוֹרֶה(i)] = temp;
            percolateUp(הוֹרֶה(i));
        }
    }
    private void percolateDown(int i){
        int l = שמאלה(i);
        int r = ימין(i);
        int min = i;
        bool flag = false;
        if(l < count && ar[i].Frequency > ar[l].Frequency){
            min = l;
            flag = true;
        }
        if(r < count && ar[r].Frequency < ar[min].Frequency){
            min = r;
            flag = true;
        }
        if(flag){
            LeafNode temp = ar[i];
            ar[i] = ar[min];
            ar[min] = temp;
            percolateDown(min);
        }
    }
    public void deleteMin(){
        if(count == 0){
            return;
        }
        if(count == 1){
            count--;
            return;
        }
        ar[0] = ar[count-1];
        count--;
        percolateDown(0);
    }
    public LeafNode getMin(){
        if(count <= 0){
            return null;
        }
        return ar[0];
    }
}