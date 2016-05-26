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
        
        
let calc xs =     
    xs |> List.exists(fun (x,d) -> 
        xs |> List.tryFind(fun (x',_) -> x'= x + d )
        |> Option.map(fun (x',d') -> x' + d' = x)
        |> (=) (Some true)  )


let print xs = 
    let s1 = xs |> List.fold (fun a (x,d) -> 
        sprintf "%s(%d,%d)" (if a="" then "" else a + " ") x d ) ""
    calc xs
    |> function true -> "YES" | _ -> "NO"
    |> printfn "%d обезьян %s - %s" (List.length xs) s1 

let rec loop() = 
    printf "введите количество обезьян или 'q'>"
    let s = Console.ReadLine().Trim()
    if s = "q" then () else 
    let b,n = Int32.TryParse( s )
    if b then
        readThrows [] 0 n 
        |> List.rev        
        |> print  
    else
        printf "некорректный ввод %A" s
    loop()




[<EntryPoint>]
do
    print [ (0,1); (1,-1) ]
    print [ (0,1); (1,1); (2,-2) ]
    print [ (2,-10); (3,-10); (0,5); (5,-5); (10,1) ]
    while true do
        printf "введите количество обезьян >"
        let n = Int32.Parse( Console.ReadLine().Trim() )
        readThrows [] 0 n 
        |> List.rev        
        |> print 

    Console.ReadKey() |> ignore

