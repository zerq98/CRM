import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import {server} from './config'

function todoList(data) {
  const [session, loading] = useSession()
  const [isOpen, setIsOpen] = useState(false)
  const [todoList, setTodoList] = useState([
    {
      "id": 0,
      "title": "Test",
      "completed": false,
      "description": "",
      "taskRange": "",
      "taskDate": new Date()
    }
  ]);

  useEffect(() => {
    if (data !== undefined) {
      setTodoList(data.data)
    }
  }, [])

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

      var today=new Date(Date.now());
      var taskDate = new Date(task.taskDate)
      console.log(today)
      console.log(taskDate)
      if (taskDate < today.getDate() && !task.completed) {
        task.taskRange = "Overdue";
      }else if (taskDate.getDate() === today.getDate()) {
        task.taskRange = "Today";
      }else if (taskDate.getDate() >= today.getDate() && taskDate.getDate() <= today.getDate() + 7) {
        task.taskRange = "This week";
      }else if (taskDate.getDate() >= today.getDate() && taskDate.getMonth() <= today.getMonth()+1) {
        task.taskRange = "This month";
      }

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

  function changeCircleSet(e) {
    e.target.className = "bx bx-check-circle"
  }
  function changeCircleUnset(e) {
    e.target.className = "bx bx-circle"
  }

  function changeTrashSet(e) {
    e.target.className = "bx bx-trash"
  }
  function changeTrashUnset(e) {
    e.target.className = "bx bxs-trash"
  }

  return (
    <Layout>
      <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-7 space-y-2">
        <div className="w-100p xs:h-100p md:h-85p xs:text-sm md:text-lg lg:text-2xl flex xs:flex-row md:flex-col xs:space-x-2 md:space-x-0 xs:space-y-0 md:space-y-2">
          <Tab.Group>
            <Tab.List className="xs:w-45p md:w-100p md:h-20 xs:h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex xs:flex-col md:flex-row items-center justify-around xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0">
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Zaległe
              </Tab>
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Dzisiejsze
              </Tab>
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Ten tydzień
              </Tab>
              <Tab className={({ selected }) =>
                selected ? "bg-opacity-45 backdrop-filter backdrop-blur-lg bg-white rounded-lg h-100p w-100p"
                  : "hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p"}>
                Ten miesiąc
              </Tab>
              <div className=" cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p flex flex-row items-center space-x-2 px-2"
                onClick={openModal}>
                <i className='bx bxs-add-to-queue'></i>
                <div>
                  Dodaj zadanie
                </div>
              </div>
            </Tab.List>
            <Tab.Panels className="h-100p xs:w-55p md:w-100p">
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain xs:space-y-4 md:space-y-0">
                {todoList.map((d, idx) => (
                  d.taskRange === "Overdue" &&
                  <div key={d.id} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 xs:h-8 md:h-20 flex flex-row space-x-2 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                    <i className='bx bx-circle hover:bx-check-circle' onClick={() => toggle(d.id)} onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i>
                    <div className="w-100p flex flex-col">
                      <div className="xs:text-sm md:text-lg md:font-bold overflow-hidden">{d.title}</div>
                      <span className="xs:text-xs md:text-md xs:hidden md:block">{(new Date(d.taskDate)).getDate()+'.'+((new Date(d.taskDate)).getMonth()+1)+'.'+(new Date(d.taskDate)).getFullYear()}</span>
                      <div className="text-lg overflow-hidden xs:hidden md:block">{d.description}</div>
                    </div>
                    <i className='bx bxs-trash' onClick={() => removeTask(d.id)} onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset}></i>
                  </div>
                ))}
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain xs:space-y-4 md:space-y-0">
                {todoList.map((d, idx) => (
                  d.taskRange === "Today" &&
                  <div key={d.id} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 xs:h-8 md:h-20 flex flex-row space-x-2 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                    {!d.completed ? <i onClick={() => toggle(d.id)} className='bx bx-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i> : <i class='bx bxs-check-circle' ></i>}
                    <div className="w-100p flex flex-col">
                      <div className="xs:text-sm md:text-lg md:font-bold">{d.title}</div>
                      <span className="xs:text-xs md:text-md xs:hidden md:block">{(new Date(d.taskDate)).getDate()+'.'+((new Date(d.taskDate)).getMonth()+1)+'.'+(new Date(d.taskDate)).getFullYear()}</span>
                      <div className="text-lg overflow-hidden xs:hidden md:block">{d.description}</div>
                    </div>
                    <i className='bx bxs-trash' onClick={() => removeTask(d.id)} onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset}></i>
                  </div>
                ))}
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain xs:space-y-4 md:space-y-0">
                {todoList.map((d, idx) => (
                  (d.taskRange === "This week" || d.taskRange === "Today") &&
                  <div key={d.id} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 xs:h-8 md:h-20 flex flex-row space-x-2 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                    {!d.completed ? <i onClick={() => toggle(d.id)} className='bx bx-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i> : <i class='bx bxs-check-circle' ></i>}
                    <div className="w-100p flex flex-col">
                      <div className="xs:text-sm md:text-lg md:font-bold">{d.title}</div>
                      <span className="xs:text-xs md:text-md xs:hidden md:block">{(new Date(d.taskDate)).getDate()+'.'+((new Date(d.taskDate)).getMonth()+1)+'.'+(new Date(d.taskDate)).getFullYear()}</span>
                      <div className="text-lg overflow-hidden xs:hidden md:block">{d.description}</div>
                    </div>
                    <i className='bx bxs-trash' onClick={() => removeTask(d.id)} onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset}></i>
                  </div>
                ))}
              </Tab.Panel>
              <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain xs:space-y-4 md:space-y-0">
                {todoList.map((d, idx) => (
                  (d.taskRange === "This month" || d.taskRange === "This week" || d.taskRange === "Today") &&
                  <div key={d.id} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 xs:h-8 md:h-20 flex flex-row space-x-2 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                    {!d.completed ? <i onClick={() => toggle(d.id)} className='bx bx-circle' onMouseOver={changeCircleSet} onMouseLeave={changeCircleUnset}></i> : <i class='bx bxs-check-circle' ></i>}
                    <div className="w-100p flex flex-col">
                      <div className="xs:text-sm md:text-lg md:font-bold">{d.title}</div>
                      <span className="xs:text-xs md:text-md xs:hidden md:block">{(new Date(d.taskDate)).getDate()+'.'+((new Date(d.taskDate)).getMonth()+1)+'.'+(new Date(d.taskDate)).getFullYear()}</span>
                      <div className="text-lg overflow-hidden xs:hidden md:block">{d.description}</div>
                    </div>
                    <i className='bx bxs-trash' onClick={() => removeTask(d.id)} onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset}></i>
                  </div>
                ))}
              </Tab.Panel>
            </Tab.Panels>
          </Tab.Group>
        </div>
        <Transition appear show={isOpen} as={Fragment}>
          <Dialog
            as="div"
            className="fixed inset-0 z-10 overflow-y-auto"
            onClose={closeModal}
          >
            <div className="min-h-screen px-4 text-center">
              <Transition.Child
                as={Fragment}
                enter="ease-out duration-300"
                enterFrom="opacity-0"
                enterTo="opacity-100"
                leave="ease-in duration-200"
                leaveFrom="opacity-100"
                leaveTo="opacity-0"
              >
                <Dialog.Overlay className="fixed inset-0" />
              </Transition.Child>
              <span
                className="inline-block h-screen align-middle"
                aria-hidden="true"
              >
                &#8203;
              </span>
              <Transition.Child
                as={Fragment}
                enter="ease-out duration-300"
                enterFrom="opacity-0 scale-95"
                enterTo="opacity-100 scale-100"
                leave="ease-in duration-200"
                leaveFrom="opacity-100 scale-100"
                leaveTo="opacity-0 scale-95"
              >
                <div className="inline-block w-full max-w-md bg-opacity-80 backdrop-filter backdrop-blur-lg p-6 my-8 overflow-hidden text-left align-middle transition-all transform bg-black shadow-xl rounded-2xl">
                  <Dialog.Title
                    as="h3"
                    className="text-lg font-medium leading-6 text-white"
                  >
                    Nowe zadanie
                  </Dialog.Title>
                  <div className="mt-2">
                    <form className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p" onSubmit={event => handleTaskAdd(event)}>
                      <input id="title" name="title" type="text" placeholder="Nazwa zadania" required
                        className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                      <input id="description" name="description" type="text" placeholder="Opis zadania"
                        className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                      <input id="taskDate" name="taskDate" type="date" defaultValue={Date.now()} required
                        className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                      <button className='bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p'
                        type='submit'>
                        Dodaj zadanie
                      </button>
                    </form>
                  </div>
                </div>
              </Transition.Child>
            </div>
          </Dialog>
        </Transition>
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
    const res = await fetch(server+"TodoTask/GetAllUserTasks", {
      method: 'GET',
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + sess.accessToken
      }
    })


    const resData = await res.json()
    const data = resData.data

    return { props: { data } }
  } else {
    context.res.writeHead(302, { Location: "/login" })
    context.res.end();
  }
  return null
}

export default todoList;