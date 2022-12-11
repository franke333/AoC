X,cycle,summ,display = 1,0,0,""

def cycle_update():
    global cycle,X,summ,display
    display += "#" if abs(cycle%40-X) <= 1 else '.'
    cycle += 1
    if (cycle-20)%40 == 0:
        summ += cycle * X
    if cycle%40 == 0: display += "\n"

for l in open("inputs/input10.txt"):
    match l.split():
        case ['noop']:
            cycle_update()
        case 'addx',x:
            cycle_update()
            cycle_update()
            X += int(x)           
print(summ)
print(display)