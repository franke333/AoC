sensors,beacons = [],set()
for l in open("inputs/input15.txt"):
    l = l.split(' ')
    x,y = int(l[2][2:-1]),int(l[3][2:-1])
    bx,by = int(l[8][2:-1]),int(l[9][2:-1])
    sensors.append((x,y,abs(x-bx)+abs(y-by)))
    beacons.add((bx,by))

def GetScannedRegionsAtRow(row):
    global sensors,beacons
    ranges,regions = [],[]
    for x,y,r in sensors:
        if (dist := abs(row-y)) <= r:
            ranges.append((x-(r-dist),x+(r-dist)))
    ranges.sort(key= lambda x : x[0])
    start,end = ranges[0]
    for s,e in ranges[1:]:
        if s <= end+1:
            end = max(end,e)
        else:
            regions.append((start,end))
            start,end = s,e
    regions.append((start,end))
    return regions
#part1
regions = GetScannedRegionsAtRow(2_000_000)
size = sum([(end - start + 1 \
    - sum([by == 2_000_000 and bx >= start and bx <= end for bx,by in beacons])) \
    for start,end in regions]) 
print(size) 
#part2
for row in range(0,4_000_001):
    if len(regions := GetScannedRegionsAtRow(row)) > 1:
        print((regions[1][0]-1) * 4_000_000 + row)
        break