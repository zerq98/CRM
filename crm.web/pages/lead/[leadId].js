import Layout from "../../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'
import { useRouter } from 'next/router'

function leadData(data) {
  const [session, loading] = useSession()
  const router = useRouter()
  const[activityIsOpen,setActivityIsOpen] = useState(false);
  const[contactIsOpen,setContactIsOpen] = useState(false);
  const[selectedActivityType,setSelectedActivityType]=useState('');
  const provinces=[
    'dolnośląskie',
    'kujawsko-pomorskie',
    'lubelskie',
    'lubuskie',
    'łódzkie',
    'małopolskie',
    'mazowieckie',
    'opolskie',
    'podkarpackie',
    'podlaskie',
    'pomorskie',
    'śląskie',
    'świętokrzyskie',
    'warmińsko-mazurskie',
    'wielkopolskie',
    'zachodnio-pomorskie'
  ]
  const statuses=[
    'Nowy',
    'Oferta',
    'Stracony',
    'Odłożony',
    'Wygrany',
    'Kontrahent'
  ]
  const[newContact,setNewContact] = useState({
    "name":"",
    "email":"",
    "phoneNumber":"",
    "department":"",
  })
  const[currentUser,setCurrentUser]=useState('');
  const [lead, setLead] = useState(
    {
      "id": 0,
      "name": "",
      "nip": "",
      "regon": "",
      "leadStatus": "",
      "leadContacts": [
        {
          "id":0,
          "name":"",
          "email":"",
          "phoneNumber":"",
          "department":"",
          "deleted":false,
          "localId":0
        }
      ],
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
      "activities": [
        {
          "id": 1,
          "activityType": "",
          "user": "",
          "activityDate": new Date(),
          "deleted": false,
          "localId": 1
        }
      ]
    }
  );
  const [tradersList, setTradersList] = useState([
    ""
  ]);

  function closeActivityModal() {
    setActivityIsOpen(false)
  }

  function openActivityModal() {
    setActivityIsOpen(true)
  }

  function closeContactModal() {
    setContactIsOpen(false)
  }

  function openContactModal() {
    setContactIsOpen(true)
  }

  function handleActivityDelete(id){
    console.log(lead)
    if(id!==undefined){
      var editedLead = {...lead,activities:lead.activities.map((act) => {
        if (act.localId === id ) {
          return {
            ...act,
            deleted:true
          }
        }
        return act
      })}
  
      setLead(editedLead)
    }
  }

  function handleContactDelete(id){
    console.log(lead)
    if(id!==undefined){
      var editedLead = {...lead,leadContacts:lead.leadContacts.map((cont) => {
        if (cont.localId === id ) {
          return {
            ...cont,
            deleted:true
          }
        }
        return cont
      })}
  
      setLead(editedLead)
    }
  }

  function handleActivityAdd(){

    if(selectedActivityType!='' && selectedActivityType!='Typ aktywności'){
      var editedLead = {...lead,activities:[...lead.activities,{
        id:0,
        activityDate:new Date(Date.now()),
        activityType:selectedActivityType,
        deleted:false,
        localId:lead.activities.length+1,
        user:currentUser
      }]}
  
      setLead(editedLead)
    }
    setSelectedActivityType('');
    closeActivityModal();
  }

  function addDataToContact(value,prop){
    switch(prop){
      case 'name':
        var contact = {
          ...newContact,
          name:value
        }
        setNewContact(contact)
        break;
      case 'email':
        var contact = {
          ...newContact,
          email:value
        }
        setNewContact(contact)
        break;
      case 'phoneNumber':
        var contact = {
          ...newContact,
          phoneNumber:value
        }
        setNewContact(contact)
        break;
      case 'department':
        var contact = {
          ...newContact,
          department:value
        }
        setNewContact(contact)
        break;
    }
  }

  function handleContactAdd(){
    if(newContact.name!=='' && newContact.email!=='' && newContact.phoneNumber!=='' && newContact.department!==''){
      var editedLead = {...lead,leadContacts:[...lead.leadContacts,{
        id:0,
        name:newContact.name,
        email:newContact.email,
        phoneNumber:newContact.phoneNumber,
        department:newContact.department,
        deleted:false,
        localId:lead.leadContacts.length+1
      }]}
  
      setLead(editedLead)
    }
    closeContactModal();
    setNewContact({
      department:'',
      email:'',
      name:'',
      phoneNumber:''
    })
  }

  function validate(){
    var isOk=true;
    
    if(lead.name==='' || lead.name===undefined){
      isOk=false
    }
    if(lead.nip==='' || lead.nip===undefined){
      isOk=false
    }
    if(lead.regon==='' || lead.regon===undefined){
      isOk=false
    }

    return isOk;
  }

  const handleLeadSave = async function(){
    if (validate()) {
      const res = await fetch("https://localhost:44395/api/Lead/Upsert", {
          method: 'POST',
          body: JSON.stringify(lead),
          headers: {
              accept: '*/*',
              "Content-Type": "application/json",
              "Authorization": "Bearer " + session.accessToken
          }
      })

      const resData = await res.json()

      if (resData.code === 201) {
          router.push('/leadList')
      } else {
          alert(resData.errorMessage)
          return 'Wrong data';
      }
  }
  }

  useEffect(() => {
    if (data !== undefined ) {
      setLead(data.data.lead)
      setTradersList(data.data.companyTraders)
      setCurrentUser(data.data.user);
    }
  }, [])

  function changeTrashSet(e) {
    e.target.className = "bx bx-trash"
  }
  function changeTrashUnset(e) {
    e.target.className = "bx bxs-trash"
  }

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG xs:p-2 lg:p-7 space-y-2 lg:text-lg xs:text-xs">
                <div className="flex md:flex-row xs:flex-col w-100p h-100p xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0">
                  <div className="flex flex-col xs:w-100p md:w-60p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-2 md:space-y-2 ">
                    <div className="flex md:flex-row xs:flex-col w-100p h-90p xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0">
                      <div className="flex md:flex-col xs:flex-row border-1 xs:w-100p xs:h-50p md:w-50p md:h-100p p-2">
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Nazwa firmy</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.name}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              name:e.target.value
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>NIP</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.nip}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              nip:e.target.value
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Regon</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.regon}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              regon:e.target.value
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Status</p>
                          <select className="w-100p pl-2 rounded-lg focus:outline-none" required
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadStatus:e.target.value
                            }
                        
                            setLead(editedLead)
                          }}>
                          {statuses.map((d,key)=>{
                              if(d===lead.leadStatus){
                                return <option selected>{d}</option>
                              }else{
                                return <option>{d}</option>
                              }
                          })}
                          </select>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Handlowiec</p>
                          <select className="w-100p pl-2 rounded-lg focus:outline-none" required 
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              user:e.target.value
                            }
                        
                            setLead(editedLead)
                          }}>
                          {tradersList.map((d,key)=>{
                              if(d===lead.user){
                                return <option selected>{d}</option>
                              }else{
                                return <option>{d}</option>
                              }
                          })}
                          </select>
                        </div>
                      </div>
                      <div className="flex md:flex-col xs:flex-row border-1 xs:w-100p xs:h-50p md:w-50p md:h-100p p-2">
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Miasto</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.leadAddress.city}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadAddress:{
                                ...lead.leadAddress,
                                city:e.target.value
                              }
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Ulica</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.leadAddress.street}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadAddress:{
                                ...lead.leadAddress,
                                street:e.target.value
                              }
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Kod pocztowy</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.leadAddress.postCode}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadAddress:{
                                ...lead.leadAddress,
                                postCode:e.target.value
                              }
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Numer domu</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" required value={lead.leadAddress.houseNumber}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadAddress:{
                                ...lead.leadAddress,
                                houseNumber:e.target.value
                              }
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Numer mieszkania (opcjonalne)</p>
                          <input className="w-100p pl-2 rounded-lg focus:outline-none" value={lead.leadAddress.apartmentNumber}
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadAddress:{
                                ...lead.leadAddress,
                                apartmentNumber:e.target.value
                              }
                            }
                        
                            setLead(editedLead)
                          }}></input>
                        </div>
                        <div className="flex flex-col xs:w-20p md:w-100p xs:h-100p md:h-20p space-y-3">
                          <p>Województwo</p>
                          <select className="w-100p pl-2 rounded-lg focus:outline-none" required
                          onChange={(e)=>{
                            var editedLead = {
                              ...lead,
                              leadAddress:{
                                ...lead.leadAddress,
                                province:e.target.value
                              }
                            }
                        
                            setLead(editedLead)
                          }}>
                            {lead.leadAddress.province!==''?null:<option selected disabled></option>}
                            {provinces.map((d,key)=>{
                              if(d===lead.leadAddress.province){
                                return <option selected>{d}</option>
                              }else{
                                return <option>{d}</option>
                              }
                            })}
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
                    <div className="flex flex-col w-100p h-50p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg space-y-2 p-2">
                      <div className="flex flex-row w-100p h-10p justify-start items-center space-x-4">
                        <p>Osoby kontaktowe</p>
                        <div className=" h-100p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow p-2 cursor-pointer"
                        onClick={openContactModal}>
                          Dodaj kontakt
                        </div>
                      </div>
                      <div className="w-100p h-90p border-1 bg-white rounded-lg flex flex-col flex-nowrap items-center overflow-y-scroll overscroll-contain scrollbar-hide p-1">
                      {lead.leadContacts.map((d, idx) => (
                        !d.deleted &&
                        <div key={d.localId} className='group rounded-lg w-100p px-2 h-16 items-center py-2 flex flex-row space-x-2 cursor-pointer bg-opacity-35 backdrop-filter backdrop-blur-lg bg-gray rounded-lg '>
                          <div className="w-100p flex flex-row justify-start space-x-3">
                            <div className="w-30p truncate">{d.name}</div>
                            <div className="w-20p truncate">{d.department}</div>
                            <div className="w-20p truncate">{d.phoneNumber}</div>
                            <div className="w-20p truncate">{d.email}</div>
                          </div>
                          <i className='bx bxs-trash' onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset} onClick={() => handleContactDelete(d.localId)}></i>
                        </div>
                      ))}
                      </div>
                    </div>
                    <div className="flex flex-col w-100p h-50p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg space-y-2 p-2">
                      <div className="flex flex-row w-100p h-10p justify-start items-center space-x-4">
                        <p>Aktywności u kontrahenta</p>
                        <div className=" h-100p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow p-2 cursor-pointer"
                        onClick={openActivityModal}>
                          Dodaj aktywność
                        </div>
                      </div>
                      <div className="w-100p h-90p border-1 bg-white rounded-lg flex flex-col flex-nowrap items-center overflow-y-scroll overscroll-contain scrollbar-hide p-1">
                      {lead.activities.map((d, idx) => (
                        !d.deleted &&
                        <div key={d.localId} className='group rounded-lg w-100p px-2 h-16 items-center py-2 flex flex-row space-x-2 cursor-pointer bg-opacity-35 backdrop-filter backdrop-blur-lg bg-gray rounded-lg '>
                          <div className="w-100p flex flex-row justify-start space-x-10">
                            <div className="w-30p">{d.activityType}</div>
                            <div className="w-20p">{(new Date(d.activityDate)).getDate()+'.'+((new Date(d.activityDate)).getMonth()+1)+'.'+(new Date(d.activityDate)).getFullYear()}</div>
                            <div className="w-30p">{d.user}</div>
                          </div>
                          <i className='bx bxs-trash w-5p' onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset} onClick={() => handleActivityDelete(d.localId)}></i>
                        </div>
                      ))}
                      </div>
                    </div>
                  </div>
                </div>
                <Transition appear show={activityIsOpen} as={Fragment}>
                  <Dialog
                    as="div"
                    className="fixed inset-0 z-10 overflow-y-auto"
                    onClose={closeActivityModal}
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
                            Nowa aktywność
                          </Dialog.Title>
                          <div className="mt-2">
                            <div className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p">
                              <select id="type" name="type" onChange={(e) => setSelectedActivityType(e.target.value)}
                                className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5">
                                  <option selected disabled>Typ aktywności</option>
                                  <option>Rozmowa telefoniczna</option>
                                  <option>Wiadomość email</option>
                                  <option>Rozmowa online</option>
                                  <option>Spotkanie z klientem</option>
                                  <option>Wysłanie oferty</option>
                                  <option>Inna aktywność</option>
                                  </select>
                              <div className='cursor-pointer bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p'
                                onClick={handleActivityAdd}>
                                Dodaj aktywność
                              </div>
                            </div>
                          </div>
                        </div>
                      </Transition.Child>
                    </div>
                  </Dialog>
                </Transition>
                <Transition appear show={contactIsOpen} as={Fragment}>
                  <Dialog
                    as="div"
                    className="fixed inset-0 z-10 overflow-y-auto"
                    onClose={closeContactModal}
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
                            Nowy kontakt
                          </Dialog.Title>
                          <div className="mt-2">
                          <div className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p">
                              <input onChange={(e)=>addDataToContact(e.target.value,'name')} placeholder="Imie i nazwisko"
                                className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5"/>
                              <input onChange={(e)=>addDataToContact(e.target.value,'email')} placeholder="E-mail"
                                className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5"/>
                              <input onChange={(e)=>addDataToContact(e.target.value,'phoneNumber')} placeholder="Numer telefonu"
                                className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5"/>
                              <input onChange={(e)=>addDataToContact(e.target.value,'department')} placeholder="Stanowisko"
                                className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5"/>
                              <div className='cursor-pointer bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p'
                                onClick={handleContactAdd}>
                                Dodaj kontakt
                              </div>
                            </div>
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