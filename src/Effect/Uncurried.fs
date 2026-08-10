let mkEffectFn1 = box (fun (f: obj) -> box (fun (a: obj) ->
    System.Console.WriteLine("mkEffectFn1 inner called")
    try
        let f' = f :?> (obj -> obj)
        let res = f' a :?> (obj -> obj)
        res null
    with e ->
        System.Console.WriteLine("mkEffectFn1 exception: " + e.ToString())
        null
))
let mkEffectFn2 = box (fun (f: obj) -> box (fun (a: obj) -> box (fun (b: obj) ->
    System.Console.WriteLine("mkEffectFn2 inner called")
    let f' = f :?> (obj -> obj)
    let fa = f' a :?> (obj -> obj)
    let fab = fa b :?> (obj -> obj)
    fab null
)))
let mkEffectFn3 = box (fun (f: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun (c: obj) ->
    System.Console.WriteLine("mkEffectFn3 called")
    let f' = f :?> (obj -> obj)
    let fa = f' a :?> (obj -> obj)
    let fab = fa b :?> (obj -> obj)
    let fabc = fab c :?> (obj -> obj)
    fabc null
))))
let mkEffectFn4 _ = undefined
let mkEffectFn5 _ = undefined
let mkEffectFn6 _ = undefined
let mkEffectFn7 _ = undefined
let mkEffectFn8 _ = undefined
let mkEffectFn9 _ = undefined
let mkEffectFn10 _ = undefined

let runEffectFn1 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun _ ->
    System.Console.WriteLine("runEffectFn1 inner called")
    try
        let eff' = eff :?> (obj -> obj)
        eff' a
    with e ->
        System.Console.WriteLine("runEffectFn1 exception: " + e.ToString())
        null
)))
let runEffectFn2 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun _ ->
    System.Console.WriteLine("runEffectFn2 inner called")
    let eff' = eff :?> (obj -> obj)
    let eff_a = eff' a :?> (obj -> obj)
    eff_a b
))))
let runEffectFn3 = box (fun (eff: obj) -> box (fun (a: obj) -> box (fun (b: obj) -> box (fun (c: obj) -> box (fun _ ->
    let eff' = eff :?> (obj -> obj)
    let eff_a = eff' a :?> (obj -> obj)
    let eff_ab = eff_a b :?> (obj -> obj)
    eff_ab c
)))))
let runEffectFn4 _ = undefined
let runEffectFn5 _ = undefined
let runEffectFn6 _ = undefined
let runEffectFn7 _ = undefined
let runEffectFn8 _ = undefined
let runEffectFn9 _ = undefined
let runEffectFn10 _ = undefined
