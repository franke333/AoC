import re; import numpy as np
inp = [l.replace('\n','') for l in open('in/04.txt').readlines()]
diag = [''.join(a.tolist()) for a in [np.array([*map(list,inp)])[::j,:].diagonal(i) for i in range(-len(inp),len(inp)) for j in [-1,1]]]
dirs = [*inp,*[''.join(a) for a in zip(*inp)],*diag]
print(sum(len([m.group(1) for m in re.finditer(r"(?=(XMAS|SAMX))",d)]) for d in dirs))
XS = [''.join([inp[y+1+dy][x+1+dx] for dx,dy in [(-1,-1),(-1,1),(0,0),(1,-1),(1,1)]]) for x in range(len(inp)-2) for y in range(len(inp)-2)]
print(sum(len(re.findall(r"MMASS|MSAMS|SSAMM|SMASM",d)) for d in XS))