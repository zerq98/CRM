import Layout from "../components/layout";
import React, { useState, useEffect } from 'react';
import { getSession,useSession } from "next-auth/client";
import { Tab } from '@headlessui/react'

function todoList(data){
  const [session, loading] = useSession()
  const[todoList,setTodoList]=useState([
    {
    "id":0,
    "title":"Test",
    "completed":false,
    "description":"",
    "taskRange":""
    }
]);

useEffect(()=>{
    if(data!== undefined){
        setTodoList(data.data)
    }
},[])

async function toggle(id){

    const res = await fetch("https://localhost:44395/api/TodoTask/MarkAsCompleted?todoTaskId="+id, {
        method: 'POST',
        body:{},
        headers: { 
          accept: '*/*',
          "Content-Type": "application/json",
          "Authorization": "Bearer "+session.accessToken
        }
      })
      const resData = await res.json()

    const completeSelectedTodo = todoList.map((todo) => {
        if (todo.id === id && resData.code===200) {
          return {
              ...todo,
              completed: true
        }
      }
      return todo
    })

    setTodoList(completeSelectedTodo)
};

function changeCircleSet(e){
  e.target.className="bx bx-check-circle"
}
function changeCircleUnset(e){
  e.target.className="bx bx-circle"
}

  return(
    <Layout>
      <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-7 space-y-2">
        <div className="w-100p h-85p">
          <Tab.Group>
            <Tab.List className="w-100p h-20 bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-row items-center justify-around space-x-2 text-2xl">
              <Tab className={({selected})=> 
              selected? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
              :"hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Zaległe
              </Tab>
              <Tab className={({selected})=> 
              selected? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
              :"hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Dzisiejsze
              </Tab>
              <Tab className={({selected})=> 
              selected? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
              :"hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Jutrzejsze
              </Tab>
              <Tab className={({selected})=> 
              selected? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
              :"hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Ten tydzień
              </Tab>
              <Tab className="hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p flex flex-row items-center space-x-2 px-2">
                <i className='bx bxs-add-to-queue'></i>
                <div>
                  Dodaj zadanie
                </div>
              </Tab>
            </Tab.List>
            <Tab.Panels className="mt-2 h-100p">
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-row items-center text-2xl">
                {todoList.map((d,idx)=>(
                  d.taskRange === "Overdue" &&
                    <div key={d.id} onClick={()=>toggle(d.id)} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 h-8 flex flex-col justify-between cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                      <i className='bx bx-circle hover:bx-check-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i>
                      <div className="text-lg">{d.title}</div>
                    </div>
                ))}
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col items-center text-2xl">
                {todoList.map((d,idx)=>(
                  d.taskRange === "Today" &&
                    <div key={d.id} onClick={()=>toggle(d.id)} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 h-8 flex flex-row justify-between cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                      {!d.completed?<i className='bx bx-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i>:<i class='bx bxs-check-circle' ></i>}
                      <div className="text-lg">{d.title}</div>
                    </div>
                ))}
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col items-center text-2xl">
                {todoList.map((d,idx)=>(
                  d.taskRange === "This week" || d.taskRange==="Today" &&
                    <div key={d.id} onClick={()=>toggle(d.id)} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 h-8 flex flex-row justify-between cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                      {!d.completed?<i className='bx bx-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i>:<i class='bx bxs-check-circle' ></i>}
                      <div className="text-lg">{d.title}</div>
                    </div>
                ))}
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col items-center text-2xl">
                {todoList.map((d,idx)=>(
                  d.taskRange === "This month" || d.taskRange === "This week" || d.taskRange==="Today" &&
                    <div key={d.id} onClick={()=>toggle(d.id)} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 h-8 flex flex-row justify-between cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                      {!d.completed?<i className='bx bx-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i>:<i class='bx bxs-check-circle' ></i>}
                      <div className="text-lg">{d.title}</div>
                    </div>
                ))}
              </Tab.Panel>
            </Tab.Panels>
          </Tab.Group>
        </div>
        
      </div>
    </Layout>
  )
}

export async function getServerSideProps(context) {
    const sess = await getSession(context)
    if(sess){
        if(process.env.NODE_TLS_REJECT_UNAUTHORIZED !== "0"){
            process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
        }
        const res = await fetch("https://localhost:44395/api/TodoTask/GetAllUserTasks", {
            method: 'GET',
            headers: { 
              accept: '*/*',
              "Content-Type": "application/json",
              "Authorization": "Bearer "+sess.accessToken
            }
          })

          const resData = await res.json()
          const data=resData.data
    
        return { props: { data } }
    }else{
        context.res.writeHead(302,{Location:"/login"})
        context.res.end();
    }
    return null
  }

export default todoList;