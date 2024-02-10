namespace FSharpImp

module BTree =   
    
    // Yield the values of a binary tree in a sequence.
    type Tree<'a> =
        | Tree of 'a * Tree<'a> * Tree<'a>
        | Leaf of 'a

    // inorder : Tree<'a> -> seq<'a>
    let rec inorder tree = seq {
        match tree with
            | Tree(x, left, right) ->
                yield! inorder left
                yield x
                yield! inorder right
            | Leaf x -> yield x
        }

    let SampleTree = Tree(6, Tree(2, Leaf(1), Leaf(3)), Leaf(9))

    [<CompiledName("Test")>]
    let test mytree = mytree |> inorder |> printfn "%A"

module rec Say =

    let arrayOfTenZeroes : int array = Array.create 10 -1
    
    let hello name =
        printfn "Hello %s" name
        arrayOfTenZeroes[5] <- 1000
        listPrt arrayOfTenZeroes[1..5]

    let add x y = x + y

    let squre x = x * x

    let listPrt items =
        for x in items do
            printfn "%A" x

    let sum list =
        let rec recursivelyAdd list acc =
            match list with
            | head :: tail -> recursivelyAdd tail (acc + head)
            | [] -> acc

        recursivelyAdd list 0

    let sampleList1 = [1..-2..-50]

    