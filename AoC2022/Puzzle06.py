inp = open("inputs/input06.txt").readline()
for part in [4,14]:
    diff_counter = 0
    data = [0]*26
    for a in inp[:part]:
        if data[ord(a)-97] == 0:
            diff_counter += 1
        data[ord(a)-97] += 1
    pos = part
    while diff_counter!=part:
        if data[ord(inp[pos])-97] == 0:
            diff_counter += 1
        data[ord(inp[pos])-97] += 1
        if data[ord(inp[pos-part])-97] == 1:
            diff_counter -= 1
        data[ord(inp[pos-part])-97] -= 1
        pos += 1
    print(pos)