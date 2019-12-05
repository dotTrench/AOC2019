module AOC2019.Day3

open System
open System.IO

type Position = int * int

type Direction =
    | Left
    | Right
    | Up
    | Down

type Move = Direction * int

let (|Prefix|_|) (p: string) (s: string) =
    if s.StartsWith(p) then
        Some(s.Substring(p.Length))
    else
        None

let parseMove input =
    match input with
    | Prefix "R" distance -> (Right, int distance)
    | Prefix "L" distance -> (Left, int distance)
    | Prefix "U" distance -> (Up, int distance)
    | Prefix "D" distance -> (Down, int distance)
    | _ -> failwithf "Invalid move %s" input

let generateWirePositions moves =
    let genMovePositions from move =
        let (fromX, fromY) = from
        let (direction, distance) = move
        let gen = match direction with
                    | Left -> (fun x -> (fromX + x, fromY))
                    | Right -> (fun x -> (fromX - x, fromY))
                    | Up -> (fun y -> (fromX, fromY + y))
                    | Down -> (fun y -> (fromX, fromY - y))
        List.init (distance + 1) gen
        |> List.skip 1


    let rec genPositions lastPos moves =
        match moves with
        | [] -> []
        | x :: xs ->
            let pos = genMovePositions lastPos x
            let last = List.last pos
            List.concat [ pos; (genPositions last xs); ]

    moves |> (genPositions (0, 0))

let parseMoves wire =
    wire
    |> Seq.map parseMove

let intersections p1 p2 =
    let other = Set.ofSeq p2
    p1
    |> Seq.filter (fun x -> (other.Contains(x)))

let manhattanDistance (x1, y1) (x2, y2) =
    abs (x1 - x2) + abs (y1 - y2)

let task1 wire1 wire2 =
    let w1Positions = wire1 |> parseMoves |> Seq.toList |> generateWirePositions
    let w2Positions = wire2 |> parseMoves |> Seq.toList |> generateWirePositions
    intersections w1Positions w2Positions
    |> Seq.map (manhattanDistance (0, 0))
    |> Seq.sort
    |> Seq.head

let readInput =
    let splitLine (line: String) =
        line.Split ','

    File.ReadAllLines "input.txt"
    |> Seq.pairwise
    |> Seq.take 1
    |> Seq.map (fun (w1, w2) -> (splitLine w1), (splitLine w2))
    |> Seq.head

[<EntryPoint>]
let main argv =
    let (wire1, wire2) = readInput
    task1 wire1 wire2 |> printfn "Task1: %A"
    0
