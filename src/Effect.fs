let pureE = box (fun (a: obj) -> box (fun _ -> a))
let bindE = box (fun (a: obj) -> box (fun (f: obj) -> box (fun _ -> 
    let a' = a :?> (obj -> obj)
    let f' = f :?> (obj -> obj)
    let a_res = a' null
    let f_res = f' a_res :?> (obj -> obj)
    f_res null
)))

let untilE = box (fun (f: obj) -> box (fun _ ->
    let f' = f :?> (obj -> obj)
    let mutable condition = false
    while not condition do
        condition <- unbox<bool> (f' null)
    null
))

let whileE = box (fun (f: obj) -> box (fun (a: obj) -> box (fun _ ->
    let f' = f :?> (obj -> obj)
    let a' = a :?> (obj -> obj)
    let mutable condition = unbox<bool> (f' null)
    while condition do
        a' null |> ignore
        condition <- unbox<bool> (f' null)
    null
)))

let forE = box (fun (lo: obj) -> box (fun (hi: obj) -> box (fun (f: obj) -> box (fun _ ->
    let f' = f :?> (obj -> obj)
    let l = unbox<int> lo
    let h = unbox<int> hi
    for i = l to h - 1 do
        let step = f' (box i) :?> (obj -> obj)
        step null |> ignore
    null
))))

let foreachE = box (fun (arr: obj) -> box (fun (f: obj) -> box (fun _ ->
    let f' = f :?> (obj -> obj)
    let arr' = unbox<obj[]> arr
    for v in arr' do
        let step = f' v :?> (obj -> obj)
        step null |> ignore
    null
)))
