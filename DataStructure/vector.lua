-- Author: Jigger
-- Date: 2021-12
-- 功能描述:数组vector

vector = {}

function vector:new(length)
  o = {}
  if length == nil or type(length) == "number" then
    length = length or 0
    if(length ~= 0) then
      for i = 1,length do
        o[i] = 0
      end
    end
  else
    error("参数错误" ,2)
  end
  setmetatable(o,self)
  self.__index=self
  return o
end

--获取长度
function vector:length(o) return #o end

--模拟尾插
function vector:push_back(v,val)
  --v[#v+1] = val
  table.insert(v,val)
end

--模拟尾弹
function vector:pop_back(v)
  table.remove(v,#v)
end

--模拟清空，不如直接在调用处=nil或={}
function vector:clear(v)
  for k in pairs (v) do 
    v[k] = nil 
  end 
end

--模拟擦除
function vector:erase(v,l,r)
  if not r then
    table.remove(v,l)--该函数会自动将后面的数据前移，而直接置空该位置不会影响后面的索引
  else
    for i=l,f do
      table.remove(v,l)
    end
  end
end

--模拟交换容器元素
function vector:swap(v1,v2)
  l,r = #v1,#v2
  o = deepCopy(v2)
  for k in pairs (v1) do 
    v2[k] = v1[k] 
  end
  if r>l then
    for i=l+1,l do v2[i] = nil end
  end
  print(#v2)
  for k in pairs (o) do 
    v1[k] = o[k] 
  end
  if l>r then
    for i=r+1,l do v1[i] = nil end
  end
  print(#v1)
end

--深拷贝数组：用于备份
function deepCopy(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end

        local new_table = {}
        lookup_table[object] = new_table
        for key, value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end

    return _copy(object)
end

v = vector:new(5)
v1 = vector:new(4)
v:swap(v,v1)
v:push_back(v,6)
print(v:length(v))
v:clear(v)--v = nil / v = {}
print(v:length(v))
return vector