inp = open("in/09.txt").readlines()[0]
front_head = 0
back_head = len(inp)-1
end_remaining = 0
checksum = 0
pos = 0
get_checksum = lambda curr_checksum, start_pos, length, ids: [int(((2*start_pos+length-1)/2) * length * ids + curr_checksum), length + start_pos]
while front_head < back_head:
    checksum, pos = get_checksum(checksum, pos, int(inp[front_head]), front_head//2)
    #print(f"{int(inp[front_head])}*{(front_head//2)}")
    #print(checksum)
    front_head += 1
    empty_space = int(inp[front_head])
    while(empty_space > 0):
        if(end_remaining == 0):
            end_remaining = int(inp[back_head])
            back_head -= 2
        fill = min(empty_space, end_remaining)
        end_remaining -= fill
        empty_space -= fill
        checksum,pos =  get_checksum(checksum,pos, fill, back_head//2 + 1)
        #print(f"{fill}*{(back_head//2 + 1)}")
        #print(checksum)
    front_head += 1
#checksum, pos = get_checksum(checksum, pos, int(inp[front_head]), front_head//2)
#print(f"{int(inp[front_head])}*{(front_head//2)}")
#print(checksum)
checksum, pos = get_checksum(checksum, pos, end_remaining, back_head//2 + 1)
#print(f"{end_remaining}*{(back_head//2 + 1)}")
print(checksum)

