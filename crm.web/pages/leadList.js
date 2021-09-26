import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'
import {server} from './config'

function leadList(data) {
  const [session, loading] = useSession()
  var filters={
    'name': '',
    'dateFrom': new Date(2000,1,1),
    'dateTo': new Date(Date.now()),
    'user': '',
    'status': '',
    'email': ''
  }
  const [leadList, setLeadList] = useState([
    {
      "id": 0,
      "name": "",
      "createDate": new Date(),
      "email": "",
      "mainContact": "",
      "user": "",
      "status": ""
    }
  ]);
  const [tradersList, setTradersList] = useState([
    ""
  ]);

  const handleFiltersChange = async function (val,filter){
    switch(filter){
      case 'name':
        filters.name=val
        break;
      case 'dateFrom':
        filters.dateFrom=new Date(val)
        break;
      case 'dateTo':
        filters.dateTo=new Date(val)
        break;
      case 'user':
        filters.user=val
        break;
      case 'status':
        filters.status=val
        break;
      case 'email':
        filters.email=val
        break;
    }
    
    const res = await fetch(server+"Lead/GetAllLeads", {
        method: 'POST',
        body:JSON.stringify(filters),
        headers: {
          accept: '*/*',
          "Content-Type": "application/json",
          "Authorization": "Bearer " + session.accessToken
        }
      })
      
      const resData = await res.json()
      if (resData.code === 200) {
        setLeadList(resData.data.leads)
      }
  }

  useEffect(() => {
    if (data !== undefined && data.data!==null) {
      setLeadList(data.data.leads)
      setTradersList(data.data.companyTraders)
    }else{
      setLeadList({})
      setTradersList({})
    }
  }, [])

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-2 space-y-2 lg:text-lg xs:text-xs">
                <div className="flex flex-row w-100p h-15p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-1 xs:flex-wrap lg:flex-nowrap">
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center rounded-lg">
                    <p>Nazwa</p>
                    <input className="focus:outline-none pl-2 w-80p" type="text" onChange={(e)=>handleFiltersChange(e.target.value,'name')}></input>
                  </div>
                  <div className="flex flex-col h-100p w-20p justify-start items-center rounded-lg xs:hidden lg:flex lg:flex-col">
                    <p>Data utworzenia</p>
                    <div className="flex flex-row space-x-1 w-90p h-30p ">
                      <p className="text-sm">Od:</p>
                      <input className="focus:outline-none pl-2 w-100p" type="date" defaultValue={filters.dateFrom} onChange={(e)=>handleFiltersChange(e.target.value,'dateFrom')}></input>
                    </div>
                    <div className="flex flex-row space-x-1 w-90p pt-1 h-30p ">
                      <p className="text-sm">Do:</p>
                      <input className="focus:outline-none pl-2 w-100p" type="date" defaultValue={filters.dateTo} onChange={(e)=>handleFiltersChange(e.target.value,'dateTo')}></input>
                    </div>
                  </div>
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center rounded-lg">
                    <p>Handlowiec</p>
                    <select className="w-80p focus:outline-none" onChange={(e)=>handleFiltersChange(e.target.value,'user')}>
                      <option selected disabled></option>
                      {tradersList.map((d, idx) => (
                        <option>{d}</option>
                      ))}
                    </select>
                  </div>
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center rounded-lg">
                    <p>Status</p>
                    <select className="w-80p focus:outline-none" onChange={(e)=>handleFiltersChange(e.target.value,'status')}>
                      <option selected disabled></option>
                      <option>Nowy</option>
                      <option>Oferta</option>
                      <option>Stracony</option>
                      <option>Odłożony</option>
                      <option>Wygrany</option>
                    </select>
                  </div>
                  <div className="h-100p w-20p justify-start items-center rounded-lg xs:hidden lg:flex lg:flex-col">
                    <p>Email</p>
                    <input className="focus:outline-none pl-2 w-80p" type="text" onChange={(e)=>handleFiltersChange(e.target.value,'email')}></input>
                  </div>
                  <Link href="/lead/0">
                    <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg">
                      <i className='bx bxs-add-to-queue'></i>
                      <div className="xs:hidden md:block">
                        Nowy lead
                      </div>
                      <div className="xs:block md:hidden">
                        Dodaj
                      </div>
                    </div>
                  </Link>
                </div>
                <div className="flex flex-row w-100p h-10p bg-opacity-75 backdrop-filter backdrop-blur-lg bg-white border-b-1 border-black rounded-lg items-center text-center xs:flex-wrap lg:flex-nowrap">
                      <div className="border-r-1 border-black w-14">ID</div>
                      <div className="border-r-1 border-black xs:w-28 md:w-52 lg:w-52">Nazwa firmy</div>
                      <div className="border-r-1 border-black w-40 xs:hidden lg:block">Data utworzenia</div>
                      <div className="border-r-1 border-black xs:w-28 md:w-52 lg:w-52">Handlowiec</div>
                      <div className="xs:border-r-0 lg:border-r-1 border-black xs:w-14 md:w-32">Status</div>
                      <div className="border-r-1 border-black w-52 xs:hidden lg:block">Osoba kontaktowa</div>
                      <div className="w-40 xs:hidden lg:block">Email</div>
                      <div className="flex-grow"></div>
                </div>
                <div className="w-100p max-h-100p h-100p flex-grow rounded-lg bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center overflow-y-scroll overscroll-contain scrollbar-hide xs:space-y-4 md:space-y-2">
                    {leadList.map((d, idx) => (
                        <Link href={"/lead/"+d.id}>
                          <div key={d.id} className='group flex flex-row w-100p h-10 py-2 border-black rounded-lg items-center text-center cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white xs:flex-wrap lg:flex-nowrap'>
                            <div className="border-r-1 border-black w-14">{d.id}</div>
                            <div className="border-r-1 border-black xs:w-28 md:w-52 lg:w-52">{d.name}</div>
                            <div className="border-r-1 border-black w-40 xs:hidden lg:block">{(new Date(d.createDate)).getDate()+'.'+((new Date(d.createDate)).getMonth()+1)+'.'+(new Date(d.createDate)).getFullYear()}</div>
                            <div className="border-r-1 border-black xs:w-28 md:w-52 lg:w-52">{d.user}</div>
                            <div className="xs:border-r-0 lg:border-r-1 border-black xs:w-14 md:w-32">{d.status}</div>
                            <div className="border-r-1 border-black w-52 xs:hidden lg:block">{d.mainContact}</div>
                            <div className="w-40 xs:hidden lg:block">{d.email}</div>
                            <div className="flex-grow"></div>
                          </div>
                        </Link>
                      ))}
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
      const res = await fetch(server+"Lead/GetAllLeads", {
        method: 'POST',
        body:JSON.stringify({
          'name': '',
          'dateFrom': new Date(2000,1,1),
          'dateTo': new Date(Date.now()),
          'user': '',
          'status': '',
          'email': ''
        }),
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

  export default leadList;