import Layout from "../components/layout";
import React, { useState, useEffect } from 'react';
import {Bar, Doughnut} from 'react-chartjs-2';
import Image from 'next/image'
import { getSession,useSession,signOut } from "next-auth/client";
import {server} from '../server'

function Dashboard(data){
    const [session, loading] = useSession()
    const [dashboardData,setDashboardData]=useState({
        "name":"",
        "position":""
    });
    const[todoList,setTodoList]=useState([
        {
        "id":0,
        "title":"Test",
        "completed":false
        }
    ]);
    const[salesData,setSalesData] = useState([
        0,
        0,
        0,
        0,
        0
    ])
    const [activityData,setActivityData]=useState([
        0,
        0,
        0,
        0,
        0,
        0
    ])

    useEffect(()=>{
        if(data!== undefined){
            setDashboardData(data.data)
            setTodoList(data.data.todoTasks)
            setActivityData(data.data.userActivity)
            setSalesData(data.data.salesData)
        }
    },[])

    async function toggle(id){

        const res = await fetch(server+"TodoTask/MarkAsCompleted?todoTaskId="+id, {
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

    const saleChances={
        labels: ['Nowa', 'Modyfikowana', 'Anulowana', 'Zaakceptowana', 'Oferta'],
        datasets: [{
          label: 'Szanse sprzedaży według statusów',
          data: salesData,
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
          'Inna aktywność',
          'Rozmowa telefoniczna',
          'Wiadomość Email',
          'Rozmowa online',
          'Spotkanie z klientem',
          'Wysłanie oferty'
      ],
      datasets: [{
        data: activityData,
        backgroundColor: [
        '#FF2222',
        '#22FF22',
        '#2222FF',
        '#CDCDCD',
        '#AAFFAA',
        '#00DDFF'
        ],
        hoverBackgroundColor: [
        '#FF0000',
        '#00FF00',
        '#0000FF',
        '#CDCDCD',
        '#AAFFAA',
        '#00DDFF'
        ],
        borderColor: [
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)',
        'rgba(0,0,0,0)'
        ],
        hoverOffset:[
        20,20,20,20,20,20
        ],
        offset:[
        10,10,10,10,10,10
        ]
      }]
      };

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-7">
                <div className="w-100p h-20 bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-row items-center justify-between">
                    <div className="flex flex-row items-center">
                        <div className="flex flex-col ml-5">
                            <div className="xs:text-sm md:text-2xl font-bold ">{dashboardData.name}</div>
                            <div className="xs:text-xs md:text-md flex flex-row justify-between">
                                <div>{dashboardData.position}</div>
                            </div>
                        </div>
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
                                              display: false
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

export async function getServerSideProps(context) {
    const sess = await getSession(context)
    if(sess){
        if(process.env.NODE_TLS_REJECT_UNAUTHORIZED !== "0"){
            process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
        }
        const res = await fetch(server+"Account/DashboardInfo", {
            method: 'GET',
            headers: { 
              accept: '*/*',
              "Content-Type": "application/json",
              "Authorization": "Bearer "+sess.accessToken
            }
          })

        const resData = await res.json()

        if(resData.code===403){
            context.res.writeHead(302,{Location:"/accessDenied"})
            context.res.end();
        }

        const data=resData.data

        if(resData.code!==200){
            alert(resData.errorMessage)
        }
    
        return { props: { data } }
    }else{
        context.res.writeHead(302,{Location:"/login"})
        context.res.end();
    }
    return null
  }

export default Dashboard;