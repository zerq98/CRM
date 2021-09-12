import Layout from "../../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'

function leadData(data) {
  const [lead, setLead] = useState(
    {
      "id": 0,
      "name": "",
      "nip": "",
      "regon": "",
      "leadStatus": "",
      "leadContacts": [],
      "leadAddress": {
        "id": 0,
        "postCode": "",
        "city": "",
        "street": "",
        "houseNumber": "",
        "apartmentNumber": "",
        "province": ""
      },
      "user": "",
      "activities": []
    }
  );
  const [tradersList, setTradersList] = useState([
    ""
  ]);

  function changeName(name) {

    const changedLead = lead
    changedLead.name=name

    setLead(changedLead)
  };

  const handleLeadSave = async function(){

  }

  useEffect(() => {
    if (data !== undefined ) {
      setLead(data.data.lead)
      setTradersList(data.data.companyTraders)
    }
  }, [])

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG xs:p-2 lg:p-7 space-y-2 lg:text-lg xs:text-xs">
                <div className="flex md:flex-row xs:flex-col w-100p h-100p xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0">
                  <div className="flex flex-col xs:w-100p md:w-60p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-2 md:space-y-2 ">
                    <div className="flex md:flex-row xs:flex-col w-100p h-90p xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0">
                      <div className="flex md:flex-col xs:flex-row border-1 xs:w-100p xs:h-50p md:w-50p md:h-100p p-2">
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Nazwa firmy</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>NIP</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Regon</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Status</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Handlowiec</p>
                          <select className="w-100p pl-2 rounded-lg focus:outline-none" required></select>
                        </div>
                      </div>
                      <div className="flex md:flex-col xs:flex-row border-1 xs:w-100p xs:h-50p md:w-50p md:h-100p p-2">
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Miasto</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Ulica</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Kod pocztowy</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Numer domu</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Numer mieszkania (opcjonalne)</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none"></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Województwo</p>
                          <select className="w-100p pl-2 rounded-lg focus:outline-none" required>
                            <option selected disabled></option>
                            <option>dolnośląskie</option>
                            <option>kujawsko-pomorskie</option>
                            <option>lubelskie</option>
                            <option>lubuskie</option>
                            <option>łódzkie</option>
                            <option>małopolskie</option>
                            <option>mazowieckie</option>
                            <option>opolskie</option>
                            <option>podkarpackie</option>
                            <option>podlaskie</option>
                            <option>pomorskie</option>
                            <option>śląskie</option>
                            <option>świętokrzyskie</option>
                            <option>warmińsko-mazurskie</option>
                            <option>wielkopolskie</option>
                            <option>zachodnio-pomorskie</option>
                        </select>
                        </div>
                      </div>
                    </div>
                    <div className="flex flex-row w-100p h-10p justify-around md:py-2 xs:py-1">
                      <div className="h-100p w-20p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow xs:p-3 md:p-2 cursor-pointer"
                      onClick={handleLeadSave}>
                        Zapisz
                      </div>
                      <Link href="../leadList">
                        <div className="h-100p w-20p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow xs:p-3 md:p-2 cursor-pointer">
                          Anuluj
                        </div>
                      </Link>
                    </div>
                  </div>
                  <div className="flex flex-col md:w-40p xs:w-100p h-100p space-y-2">
                    <div className="flex flex-col w-100p h-50p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg">
                    </div>
                    <div className="flex flex-col w-100p h-50p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg">
                    </div>
                  </div>
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
      const res = await fetch("https://localhost:44395/api/Lead/GetLead?leadId="+context.params.leadId, {
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

  export default leadData;