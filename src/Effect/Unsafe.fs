let unsafePerformEffect = 
    fun (fVal: obj) ->
        let f = fVal :?> (obj -> obj)
        f null
