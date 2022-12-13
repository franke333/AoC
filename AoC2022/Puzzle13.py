from ast import literal_eval
from functools import cmp_to_key
import itertools

def compare(x,y):
    if type(x) != type(y):
            return compare([x],y) if type(x) is int else compare(x,[y])
    else: # same type
        if type(x) is int: # both ints
            if x != y:
                return -1 if x > y else 1
            return 0
        else: # both arrays
            for i in range(min(len(x),len(y))):
                if (res := compare(x[i],y[i])) != 0:
                    return res
            if len(x) != len(y):
                return -1 if len(x) > len(y) else 1
            return 0
                
#part 1
with open('inputs/input13.txt') as f:
    index = 1
    summ = 0
    for line1,line2,_ in itertools.zip_longest(*[f]*3):
        if compare(literal_eval(line1),literal_eval(line2)) != -1:
            summ+= index
        index += 1
    print(summ)

#part 2
signals = []
for l in open('inputs/input13.txt'):
    if l[:-1] != "":
        signals.append(literal_eval(l))
dividers = [[[2]],[[6]]]
for div in dividers:
    signals.append(div)
sorted_signals = sorted(signals,key=cmp_to_key(lambda x,y : -compare(x,y)))
print((sorted_signals.index(dividers[0])+1)*(sorted_signals.index(dividers[1])+1))