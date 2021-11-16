import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'
import {server} from './config'

function opportunities(data) {
  const [session, loading] = useSession()
  var filters={
    'dateFrom': new Date(2000,1,1),
    'dateTo': new Date(Date.now()),
    'user': '',
    'status': '',
    'lead': ''
  }
  const [oppoList, setOppoList] = useState([
    {
      "id": 0,
      "lead": "",
      "createDate": new Date(),
      "sumNetValue": 0,
      "sumGrossValue": 0,
      "sumMarkupValue": 0,
      "sumVatValue": 0,
      "trader": "",
      "status": "",
      "positions":[
        {
          "id":0,
          "product":"",
          "quantity":0,
          "netValue": 0,
          "grossValue": 0,
          "markupValue": 0,
          "vatValue": 0,
        }
      ]
    }
  ]);
  const [tradersList, setTradersList] = useState([
    ""
  ]);

  const handleFiltersChange = async function (val,filter){
    switch(filter){
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
      case 'lead':
        filters.email=val
        break;
    }
    
    const res = await fetch(server+"Opportunity/GetAllOrders", {
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
        setOppoList(resData.data.sellOpportunities)
      }else{
        alert(resData.errorMessage)
      }
  }

  useEffect(() => {
    if (data !== undefined && data.data!==null) {
      setOppoList(data.data.sellOpportunities)
      setTradersList(data.data.traderList)
    }else{
      setOppoList({})
      setTradersList({})
    }
  }, [])

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-2 space-y-2 lg:text-lg xs:text-xs">
                <div className="flex flex-row w-100p h-15p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-1 xs:flex-wrap lg:flex-nowrap">
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
                  <div className="h-100p w-20p justify-start items-center rounded-lg flex flex-col">
                    <p>Lead</p>
                    <input className="focus:outline-none pl-2 w-80p" type="text" onChange={(e)=>handleFiltersChange(e.target.value,'lead')}></input>
                  </div>
                </div>
                <div className="flex flex-row w-100p h-10p bg-opacity-75 backdrop-filter backdrop-blur-lg bg-white border-b-1 border-black rounded-lg items-center text-center xs:flex-wrap lg:flex-nowrap">
                      <div className="border-r-1 border-black w-14">ID</div>
                      <div className="border-r-1 border-black xs:w-28 md:w-28 lg:w-48">Lead</div>
                      <div className="border-r-1 border-black w-40 xs:hidden lg:block">Data utworzenia</div>
                      <div className="border-r-1 border-black xs:w-28 md:w-28 lg:w-48">Handlowiec</div>
                      <div className="border-r-1 border-black xs:w-10 md:w-28">Status</div>
                      <div className="border-r-1 border-black xs:w-20 md:w-24 lg:w-36">Cena brutto</div>
                      <div className="xs:w-20 md:w-24 lg:w-36">Marża</div>
                      <div className="flex-grow"></div>
                </div>
                <div className="w-100p max-h-100p h-100p flex-grow rounded-lg bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center overflow-y-scroll overscroll-contain scrollbar-hide xs:space-y-4 md:space-y-2">
                    {oppoList.map((d, idx) => (
                        <Link href={"/order/"+d.id}>
                          <div key={d.id} className='group flex flex-row w-100p h-10 py-2 border-black rounded-lg items-center text-center cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white xs:flex-wrap lg:flex-nowrap'>
                            <div className="border-r-1 border-black w-14 truncate">{d.id}</div>
                            <div className="border-r-1 border-black xs:w-28 md:w-28 lg:w-48 truncate">{d.lead}</div>
                            <div className="border-r-1 border-black w-40 xs:hidden lg:block truncate">{(new Date(d.createDate)).getDate()+'.'+((new Date(d.createDate)).getMonth()+1)+'.'+(new Date(d.createDate)).getFullYear()}</div>
                            <div className="border-r-1 border-black xs:w-28 md:w-28 lg:w-48 truncate">{d.trader}</div>
                            <div className="border-r-1 border-black xs:w-10 md:w-28 truncate">{d.status}</div>
                            <div className="border-r-1 border-black xs:w-20 md:w-24 lg:w-36 truncate">{d.sumGrossValue}</div>
                            <div className="xs:w-20 md:w-24 lg:w-36 truncate">{d.sumMarkupValue}</div>
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
      const res = await fetch(server+"Opportunity/GetAllOrders", {
        method: 'POST',
        body:JSON.stringify({
          'dateFrom': new Date(2000,1,1),
          'dateTo': new Date(Date.now()),
          'trader': '',
          'status': '',
          'leadName': ''
        }),
        headers: {
          accept: '*/*',
          "Content-Type": "application/json",
          "Authorization": "Bearer " + sess.accessToken
        }
      })
  
      
      const resData = await res.json()
      const data = resData.data
  
      if(resData.code!==200){
        alert(resData.errorMessage)
      }

      return { props: { data } }
    } else {
      context.res.writeHead(302, { Location: "/login" })
      context.res.end();
    }
    return null
  }

  export default opportunities;