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




[<EntryPoint>]
do
    
    while true do
        printf "введите количество обезьян >"
        let n = Int32.Parse( Console.ReadLine().Trim() )
        readThrows [] 0 n 
        |> List.rev        
        |> calc
        |> function true -> "YES" | _ -> "NO"
        |> printfn "ответ - %s"

    Console.ReadKey() |> ignore

