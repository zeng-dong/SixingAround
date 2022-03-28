using System.Reactive.Linq;

var query = from number in Enumerable.Range(1, 10) select number;
var oq = query.ToObservable();

oq.Subscribe(Console.WriteLine, () => { Console.WriteLine("I am done."); });