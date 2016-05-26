open System

let rec readThrows acc n mn= 
    if n = mn then acc else
    printfn "введите бросок обезьяны №%d >" (n+1)
    let s = Console.ReadLine().Trim()
    try
        
        if s ="q" then acc else

        let [|x;d|] = 
            s.Split([|" "; "\r";"\n"|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map Int32.Parse

        
        readThrows ( (x,d)::acc ) (n+1) mn
    with e -> 
        printfn "неправильный ввод %A" s
        readThrows acc n mn
        
        

[<EntryPoint>]
do
    printf "введите количество обезьян >"
    let n = Int32.Parse( Console.ReadLine().Trim() )
    let xs = 
        readThrows [] 0 n 
        |> List.rev
        |> List.mapi(fun i (x,d) -> i,x,d)
    xs 
    |> List.exists(fun (i,x,d) -> 
        let n = x + d
        if n >= xs.Length then false else
        let _,x1,d1 = xs.[n]
        x1 + d1 = i )
    |> printfn "%A"

    Console.ReadKey() |> ignore

