-- Author: Jigger
-- Date: 2021-12
-- 功能描述:单链表list

list={}
local node={}

function node.new(val)
  return {data=val,next=nil}   --data表示数据域 next表示指针域
end

--insert:在某个节点后插入新节点
function node.insert(n,val)

    n.next=node.new(val)
end


