import Layout from "../components/layout";
import React, { useState, useEffect } from 'react';
import {Bar, Doughnut} from 'react-chartjs-2';
import Image from 'next/image'
import { getDashboardData } from "./api/account";
import Cookies from "cookies";

var executed = false

function Dashboard(data){
    const [dashboardData,setDashboardData]=useState({
        "name":"",
        "department":"",
        "position":""
    });
    const[todoList,setTodoList]=useState([
        {
        "id":0,
        "title":"",
        "completed":false
        }
    ]);

    useEffect(()=>{
        if(data!== undefined){
            setDashboardData(data.data)
            setTodoList(data.data.todoTasks)
        }
    })
    console.log(data)

    function toggle(id){
        const completeSelectedTodo = todoList.map((todo) => {
            if (todo.id === id) {
              return {
                  ...todo,
                  completed: true
                }
            }
            return todo
        })
    
        setTodoList(() => completeSelectedTodo );
    };

    const saleChances={
        labels: ['Nowa', 'Analiza', 'Oferta', 'Zamknięta', 'Stracona'],
        datasets: [{
          label: 'Szanse sprzedaży według statusów',
          data: [12, 19, 3, 5, 2],
          backgroundColor: [
            'rgba(0, 0, 255, 1)',
            'rgba(255, 69, 0, 1)',
            'rgba(255, 255, 0, 1)',
            'rgba(0, 255, 0, 1)',
            'rgba(255, 0, 0, 1)',
          ],
          borderWidth: 0,
        }]
    }

    const contactTypes = {
        labels: [
          'Telefon',
          'Email',
          'Rozmowa online',
          'Spotkanie z klientem'
      ],
      datasets: [{
        data: [300, 50, 100,30],
        backgroundColor: [
        '#FF2222',
        '#22FF22',
        '#2222FF',
        '#CDCDCD'
        ],
        hoverBackgroundColor: [
        '#FF0000',
        '#00FF00',
        '#0000FF',
        '#CDCDCD'
        ],
        borderColor: [
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        ],
        hoverOffset:[
        20,20,20,20
        ],
        offset:[
        10,10,10,10
        ]
      }]
      };

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-7">
                <div className="w-100p h-20 bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-row items-center justify-between">
                    <div className="flex flex-row items-center">
                        <div className="h-16 m-2 w-16 relative ">
                            <Image
                                src="/blank-profile-picture.svg"
                                alt="Employee image"
                                layout="fill"
                                objectFit="cover"
                                className="rounded-full"
                            />
                        </div>
                        <div className="flex flex-col">
                            <div className="xs:text-sm md:text-2xl font-bold ">{dashboardData.name}</div>
                            <div className="xs:text-xs md:text-md flex flex-row justify-between">
                                <div>{dashboardData.position}</div>
                                <div className="xs:block md:hidden">
                                    Dział: <span className="font-bold ">{dashboardData.department}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="xs:hidden md:block text-xl mx-2 flex flex-row">
                        Dział: <span className="font-bold ml-2">{dashboardData.department}</span>
                    </div>
                </div>
                <div className="w-100p h-90p flex flex-row md:space-x-2 items-stretch">
                    <div className="xs:w-100p md:w-60p mt-2 flex flex-col space-y-2 ">
                        <div className="w-100p h-50p">
                            <Bar
                                data={saleChances}
                                width='100%'
                                height='100%'
                                className=" bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-1"
                                options={{
                                    maintainAspectRatio: false,
                                    plugins:{
                                        legend:{
                                            display: true,
                                            labels:{
                                                boxWidth:0,
                                                boxHeight:0,
                                                color: '#000000'
                                            }
                                        }
                                    },
                                    scales: {
                                        x: {
                                          grid: {
                                            display: false
                                          }
                                        },
                                        y: {
                                          grid: {
                                            color: '#000000'
                                          },
                                          ticks:{
                                              color: '#000000'
                                          }
                                        }
                                      },
                                    responsive: true
                                }}
                            />
                        </div>
                        <div className="w-100p h-50p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg">
                            <Doughnut
                                data={contactTypes}
                                width='100%'
                                height='100%'
                                className="p-3"
                                options={{
                                    maintainAspectRatio: false,
                                }}
                            />
                        </div>
                    </div>
                    <div className="w-40p mt-2 flex flex-col bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg xs:hidden md:block">
                        <div className="w-100p px-2 h-10 flex flex-row items-center align-center border-b">
                            <div className="text-lg font-bold">
                                Zadania na dzisiaj: {todoList.length}
                            </div>
                        </div>
                        {todoList.map((d,idx)=>(
                            d.completed===false &&
                            <div key={d.id} onClick={()=>toggle(d.id)} className='group rounded-lg w-90p mx-2 px-2 items-center py-2 h-8 flex flex-row justify-between cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white'>
                                <div className="text-lg">{d.title}</div>
                                <div className="text-markAsRead hidden group-hover:block">Oznacz jako ukończone</div>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </Layout>
    )
}

export async function getServerSideProps({ req, res }) {
    const cookies = new Cookies(req, res)
    var userId = cookies.get('userId');
    var token = cookies.get('userToken');

    const response = await getDashboardData(userId,token)
    const data = response
  
    return { props: { data } }
  }

export default Dashboard;