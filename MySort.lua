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
  
end
MySort:Bubble_Sort({8,99,10,3,1,4})