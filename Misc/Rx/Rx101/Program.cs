using System.Reactive.Linq;

(from number in Enumerable.Range(1, 10) select number).ToObservable()
    .Subscribe(
        Console.WriteLine,
        () => Console.WriteLine("I am done.")
    );

/// IObservable<int> xs =

Observable.Range(1, 10).Subscribe(
    i => Console.WriteLine(i * 3),
    () => Console.WriteLine("I am done.")
);