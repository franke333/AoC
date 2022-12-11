import numpy as np
inp = []
for l in open("inputs/input08.txt").readlines():
    inp.append([])
    for c in l[:-1]:
        inp[-1].append(int(c))
size = len(inp)
blocked_sides = np.zeros((size,size))
ranges = [range(size),range(size-1,-1,-1)]
for r in ranges:
    for i in r:
        tree = -1
        for j in r:
            if tree >= inp[j][i]:
                blocked_sides[j][i] += 1
            else:
                tree = inp[j][i]
for r in ranges:
    for j in r:
        tree = -1
        for i in r:
            if tree >= inp[j][i]:
                blocked_sides[j][i] += 1
            else:
                tree = inp[j][i]
print(sum(sum(x < 4 for x in blocked_sides)))

scenic_score = 0
for x in range(1,size-1):
    for y in range(1,size-1):
        t_b = inp[x][y]
        t_b_scenic = 1
        tmp = 0
        for t in inp[x][y+1::1]:
            tmp += 1
            if t_b <= t:
                break
        t_b_scenic*= tmp
        tmp = 0
        for t in inp[x][y-1::-1]:
            tmp += 1
            if t_b <= t:
                break
        t_b_scenic*= tmp
        tmp = 0
        for t in inp[x+1::1]:
            tmp += 1
            if t_b <= t[y]:
                break
        t_b_scenic*= tmp
        tmp = 0
        for t in inp[x-1::-1]:
            tmp += 1
            if t_b <= t[y]:
                break
        t_b_scenic*= tmp
        tmp = 0
        if t_b_scenic > scenic_score:
            scenic_score = t_b_scenic
print(scenic_score)
