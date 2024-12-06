module Day1

type EqualLengthLists =
    private
    | EqualLengthLists of int list * int list

    static member Create(list1: int list, list2: int list) =
        if List.length list1 = List.length list2 then
            Some(EqualLengthLists(list1, list2))
        else
            None

let distance (EqualLengthLists(list1, list2)) =
    let rec d l1 l2 =
        match l1, l2 with
        | head1 :: tail1, head2 :: tail2 -> abs (head1 - head2) + d tail1 tail2
        | [], [] -> 0
        | _, _ -> failwith "Unexpected case. Program should never reach here if lists are equal length." // exhaustive

    d (List.sort list1) (List.sort list2)




let test () =
    let ls = EqualLengthLists.Create([ 3; 4; 2; 1; 3 ], [ 4; 3; 5; 3; 9; 3 ])
    //let ls = EqualLengthLists.Create([ 3; 4; 2; 1; 3; 3 ], [ 4; 3; 5; 3; 9; 3 ])

    match ls with
    | Some lists ->
        printfn "True"
        distance lists = 11
    | None ->
        printfn "Lists must be of equal length"
        false



let main () =
    let inputPath = "inputs/day1-input1"

    let getInput filePath =
        let lines = Array.toList (System.IO.File.ReadAllLines inputPath)

        let mutable leftList = []
        let mutable rightList = []

        for line in lines do
            let cols = line.Split("   ")
            leftList <- int cols[0] :: leftList
            rightList <- int cols[1] :: rightList


        leftList, rightList


    let l1, l2 = getInput inputPath
    let ls = EqualLengthLists.Create(l1, l2)

    match ls with
    | Some lists -> printfn "%d" (distance lists)
    | None -> printfn "Lists must be of equal length"
