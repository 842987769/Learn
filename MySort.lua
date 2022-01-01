-- Author: Jigger
-- Date: 2021-12
-- 功能描述:Lua排序算法合集

MySort={}

--输出结果
local function out(List)
  ans=''
  for k,v in ipairs(List) do
    ans=ans..' '..v
  end
  print(ans)
end

--冒泡排序
function MySort:Bubble_Sort(List, func)
  func=func or function(x,y) return x<y end
  for i=1,#List do
    local ifchange=true
    for j=1,#List-i do
      if not func(List[j],List[j+1]) then
        List[j],List[j+1]= List[j+1],List[j]
        ifchange=false
      end
    end
    if ifchange then
      break
    end
  end
  out(List)
end

--选择排序
function MySort:Selection_Sort(List, func)
  func=func or function(x,y) return x<y end
  for i=1,#List do
    local cur=i
    for j=i+1,#List do
      if not func(List[cur],List[j]) then
        cur=j
      end
    end
    List[i],List[cur]=List[cur],List[i]
  end
  out(List)
end

--插入排序
function MySort:Insert_Sort(List, func)
  func=func or function(x,y) return x<y end
  for i=2,#List do
    local cur=i
    for j=i-1,1,-1 do
      if func(List[cur],List[j]) then
        List[cur],List[j]=List[j],List[cur]
        cur=j
      else
        break
      end
    end
  end
  out(List)
end

--快速排序
function MySort:Quick_Sort(List,l,r,func)
  func=func or function(x,y) return x<y end
  l=l or 1
  r=r or #List
  if List==nil then return end
  if l>=r then return end
  local index=math.random(r-(l-1))+l-1
  List[l],List[index]=List[index],List[l]
  local key=List[l]
  index=l
  local cur=l+1
  while cur<=r do
    if func(List[cur],key) then
      index=index+1
      List[cur],List[index]=List[index],List[cur]  
    end
    cur=cur+1
  end
  List[l],List[index]=List[index],List[l]
  MySort:Quick_Sort(List,l,index-1,func)
  MySort:Quick_Sort(List,index+1,r,func)
  print('快排')
  out(List)
end

--测试数据
MySort:Bubble_Sort({8,99,10,10,3,1,4})
MySort:Selection_Sort({8,99,10,10,3,1,4})
MySort:Insert_Sort({8,99,10,10,3,1,4})
MySort:Quick_Sort({8,99,10,10,3,1,4})
return MySort