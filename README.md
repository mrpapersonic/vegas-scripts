Hi, this is where I put (most of) my Vegas Pro scripts

Each file assumes that you're using version 14 or up. There is one *major* exception, which is the disable resample script. It doesn't make the check because resample can be disabled project-wide in 14 and up anyway, so I've just made it 13-compatible from the get-go

But if you do use a Sony version of Vegas, you can just remove the `#define VER_GEQ_14` and it'll work just fine