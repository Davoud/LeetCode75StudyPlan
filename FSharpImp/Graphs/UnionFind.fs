module FSharpImp.Graphs.UnionFind

type SetUnion = {
    parent: int array
    size: int array 
}           

let newSetUnion n = { 
    parent = [| 0 .. n - 1 |]; 
    size = Array.create n 1 
}

let rec find (set: SetUnion) (x: int) = if set.parent[x] = x then x else find set set.parent[x]        

let union (set: SetUnion) (s1: int) (s2: int) = 
    let r1, r2 = find set s1, find set s2
    if r1 <> r2 then
        if set.size[r1] >= set.size[r2] then
            set.size[r1] <- set.size[r1] + set.size[r2]
            set.parent[r2] <- r1
        else
            set.size[r2] <- set.size[r1] + set.size[r2]
            set.parent[r1] <- r2

let inSameComponent (set: SetUnion) (s1: int) (s2: int) = find set s1 = find set s2

