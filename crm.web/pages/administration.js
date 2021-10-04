import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import {server} from './config'

function todoList(data) {
  const [session, loading] = useSession()
  const [isOpen, setIsOpen] = useState(false)
  const [userList, setUserList] = useState([
    {
      "id": 0,
      "name": "Jan Kowalski",
      "department": "IT Leader",
      "gender": "male",
      "startDate": "01.01.2021"
    },
    {
        "id": 0,
        "name": "Janina Kowalska",
        "department": "Główna kadrowa",
        "gender": "female",
        "startDate": "01.02.2021"
    }
  ]);

//   useEffect(() => {
//     if (data !== undefined) {
//       setUserList(data.users)
//     }
//   }, [])

  function closeModal() {
    setIsOpen(false)
  }

  function openModal() {
    setIsOpen(true)
  }

  async function handleTaskAdd(event) {
    event.preventDefault();
    event.stopPropagation();

    console.log(event)
    const task = {
      title: event.target.title.value,
      description: event.target.description.value,
      taskDate: event.target.taskDate.value,
      taskRange: '',
      completed: false
    }


    const res = await fetch(server+"TodoTask/AddNewTask", {
      method: 'POST',
      body: JSON.stringify(task),
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + session.accessToken
      }
    })

    const resData = await res.json()

    if (resData.code === 201) {
      const completeTodo = todoList.map((todo) => {
        return todo
      })

      task.taskRange=resData.data.taskRange
      task.id=resData.data.id

      console.log(task)
      completeTodo.push(task)
      setTodoList(completeTodo)
      alert('Dodano nowe zadanie')
      closeModal();
    } else {
      alert(resData.errorMessage)
    }
  }

  async function toggle(id) {

    const res = await fetch(server+"TodoTask/MarkAsCompleted?todoTaskId=" + id, {
      method: 'POST',
      body: {},
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + session.accessToken
      }
    })
    const resData = await res.json()

    const completeSelectedTodo = todoList.map((todo) => {
      if (todo.id === id && resData.code === 200) {
        return {
          ...todo,
          completed: true
        }
      }
      return todo
    })

    setTodoList(completeSelectedTodo)
  };

  async function removeTask(id){
    
    const res = await fetch(server+"TodoTask/Remove?todoTaskId=" + id, {
      method: 'POST',
      body: {},
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + session.accessToken
      }
    })
    const resData = await res.json()

    if(resData.code === 200){
      setTodoList(todoList.filter(function (task){
        return task.id!==id
      }))
    }
  }

  function changeTrashSet(e) {
    e.target.className = "bx bx-trash"
  }
  function changeTrashUnset(e) {
    e.target.className = "bx bxs-trash"
  }

  return (
    <Layout>
      <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-2 space-y-2">
        <div className="w-100p h-100p xs:text-sm md:text-lg lg:text-2xl flex flex-col space-y-2">
          <Tab.Group>
            <Tab.List className="w-100p h-20 bg-opacity-35 p-2 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex xs:flex-col md:flex-row items-center justify-around xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0 overflow-auto overscroll-contain scrollbar-hide">
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Statystyki
              </Tab>
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Użytkownicy
              </Tab>
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Dane firmy
              </Tab>
            </Tab.List>
            <Tab.Panels className="h-100p w-100p">
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain xs:space-y-4 md:space-y-0">
                
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center p-2 scrollbar-hide overflow-auto overscroll-contain space-y-2">
                {userList.map((user)=>(
                    <div className="flex flex-row w-100p items-center space-x-4 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg">
                        {user.gender==='male'?<i class='bx bx-male'></i>:<i class='bx bx-female'></i>}
                        <div className="flex flex-col">
                            <div>{user.name}</div>
                            <div className="flex flex-row space-x-2">
                                <div className="xs:text-xxs md:text-sm lg:text-md">{user.department}</div>
                                <div className="xs:text-xxs md:text-sm lg:text-md">Rozpoczęcie: {user.startDate}</div>
                            </div>
                        </div>
                    </div>
                ))}
                <div className="flex flex-row w-100p items-center space-x-4 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg">
                    <i class='bx bx-plus'></i>
                    <div>Dodaj użytkownika</div>
                </div>
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain xs:space-y-4 md:space-y-0">
                
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
  if (sess) {
    if (process.env.NODE_TLS_REJECT_UNAUTHORIZED !== "0") {
      process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
    }
    // const res = await fetch(server+"Administration/GetCompanyData", {
    //   method: 'GET',
    //   headers: {
    //     accept: '*/*',
    //     "Content-Type": "application/json",
    //     "Authorization": "Bearer " + sess.accessToken
    //   }
    // })


    // const resData = await res.json()
    // const data = resData.data

    return { props: {} }
  } else {
    context.res.writeHead(302, { Location: "/login" })
    context.res.end();
  }
  return null
}

export default todoList;