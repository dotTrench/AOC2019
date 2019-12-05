module AOC2019.Day2

open System
open System.IO

type Parameters = int * int * int

type Operator =
    | Add of Parameters
    | Multiply of Parameters
    | Exit

type Tape = list<int>

let replace (tape: Tape) index newValue: Tape =
    List.concat [ tape.[..index - 1]; [ newValue ]; tape.[index + 1..] ]

let getValue (tape: Tape) position =
    tape.[position]

let parseSection (tape: Tape) index: Parameters =
    int (tape.[index + 1]), int (tape.[index + 2]), int (tape.[index + 3])

let add (tape: Tape) ((i1, i2, ix): Parameters) =
    let v1 = getValue tape i1
    let v2 = getValue tape i2
    let res = v1 + v2 
    replace tape ix res
    
let multiply (tape: Tape) ((i1, i2, out): Parameters) =
    let res = getValue tape i1 * getValue tape i2
    replace tape out res

let parseOperator (tape: Tape) index: Operator =
    let opCode = tape.[index]
    let parseSection tape =
        parseSection tape index
    match opCode with
    | 99 -> Exit
    | 1 -> Add(parseSection tape)
    | 2 -> Multiply(parseSection tape)
    | _ -> raise (ArgumentException())

let executeTape (tape: Tape) =
    let rec executeSection (tape: Tape) index =
        let operator = parseOperator tape index
        
        match operator with
        | Exit -> tape
        | Add(section) ->        
            let newTape = add tape section
            executeSection newTape (index + 4)
            
        | Multiply(section) ->
            let newTape = multiply tape section
            executeSection newTape (index + 4)

    executeSection tape 0

let t1PreStart tape =
    let x = replace tape 1 12
    replace x 2 2
    
let run tape noun verb =
    replace tape 1 noun
    |> fun x -> replace x 2 verb
    |> executeTape
    |> List.head
    
let task1 tape =
     run tape 12 2

let task2 tape =
    let combinations = [
        for verb in [0..100] do
        for noun in [0..100] do
        yield (noun, verb)
    ]
    
    let runCombination (noun, verb) =
        run tape noun verb
    
    let tryCombination (noun, verb) =
        (runCombination (noun, verb)) = 19690720

    let combine (noun, verb) =
        100 * noun + verb
        
    combinations
    |> Seq.filter tryCombination
    |> Seq.map combine
    |> Seq.head


let readTape: Tape =
    File.ReadAllText "input.txt"
    |> (fun it -> it.Split ',')
    |> Array.map int
    |> Array.toList

[<EntryPoint>]
let main argv =
    let tape = readTape
    task1 tape |> printfn "T1: %A"
    task2 tape |> printfn "T2: %A"
    0 // return an integer exit code
