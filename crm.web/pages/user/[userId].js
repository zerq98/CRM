import Layout from "../../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'
import { useRouter } from 'next/router'
import {server} from '../config'

function oppoData(data) {
  const [session, loading] = useSession()
  const key_strings = {
    lowercase: 'abcdefghijklmnopqrstuvwxyz',
    uppercase: 'ABCDEFGHIJKLMNOPQRSTUVWXYZ',
    number: '0123456789',
    symbol: "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~",
  };
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
    'zachodniopomorskie'
  ]

  function generatePassword() {
    var passwordCharSet = "";

    passwordCharSet += key_strings.lowercase;
    passwordCharSet += key_strings.uppercase;
    passwordCharSet += key_strings.symbol;
    passwordCharSet += key_strings.number;

    var password = "";
    for (let i = 0; i < 10; i++) {
      password += passwordCharSet[Math.floor(Math.random() * passwordCharSet.length)]
    }
    var userPass = {...user,password:password}
    console.log(password)
    setUser(userPass)
  }
  const router = useRouter()
  const [user,setUser] = useState({
      "id":"",
      "firstName":"",
      "lastName":"",
      "email":"",
      "login":"",
      "phoneNumber":"",
      "password":"",
      "department":"",
      "gender":false,
      "address":{
          "postCode":"",
          "city":"",
          "houseNumber":"",
          "apartmentNumber":"",
          "street":"",
          "province":""
      },
      "permissions":[
      ]
  })

  const handleUserSave = async function(){
    const res = await fetch(server+"Administration/UpsertUser", {
        method: 'POST',
        body: JSON.stringify(user),
        headers: {
            accept: '*/*',
            "Content-Type": "application/json",
            "Authorization": "Bearer " + session.accessToken
        }
    })

    const resData = await res.json()

    if (resData.code === 201) {
        router.push('/administration')
    } else {
        alert(resData.errorMessage)
        return 'Wrong data';
    }
  }

  useEffect(() => {
    if (data !== undefined) {
        setUser(data.data)
    }else{
        var newUser = {
            "id":"",
            "firstName":"",
            "lastName":"",
            "email":"",
            "login":"",
            "phoneNumber":"",
            "password":"",
            "department":"",
            "gender":false,
            "address":{
                "postCode":"",
                "city":"",
                "houseNumber":"",
                "apartmentNumber":"",
                "street":"",
                "province":""
            },
            "permissions":[
              {
                  "name":"Dodawanie użytkowników",
                  "selected":false
              },
              {
                  "name":"Usuwanie użytkowników",
                  "selected":false
              },
              {
                  "name":"Panel administracji",
                  "selected":false
              },
              {
                  "name":"Przeglądanie leadów",
                  "selected":false
              },
              {
                  "name":"Modyfikacja cudzych leadów",
                  "selected":false
              },
              {
                  "name":"Przeglądanie szans sprzedaży",
                  "selected":false
              },
              {
                  "name":"Modyfikacja cudzych szans sprzedaży",
                  "selected":false
              },
              {
                  "name":"Przeglądanie szans sprzedaży",
                  "selected":false
              },
              {
                  "name":"Przeglądanie produktów",
                  "selected":false
              },
              {
                  "name":"Modyfikacja produktów",
                  "selected":false
              }
            ]
        }
        setUser(newUser)
    }
  }, [])
    return(
        <Layout>
            <div className="w-100p h-100p items-center bg-layoutBG p-7 lg:text-lg xs:text-xs overflow-y-scroll overscroll-contain scrollbar-hide">
                <div className="flex flex-col items-center space-y-2 w-100p h-100p items-center p-2 bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg">
                    <div className="flex md:flex-row xs:flex-col w-100p xl:h-28 xs:h-36 md:space-y-0 xs:space-y-6 md:space-x-4 xs:space-x-0 border-1 p-3 overflow-y-scroll overscroll-contain scrollbar-hide">
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Imie</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.firstName} onChange={(e)=>{
                                var editedUser={...user,firstName:e.target.value}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Nazwisko</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.lastName} onChange={(e)=>{
                                var editedUser={...user,lastName:e.target.value}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-30p md:w-15p xs:w-100p">
                            <p>Hasło</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.password} onChange={(e)=>{
                                var editedUser={...user,password:e.target.value}
                                setUser(editedUser)
                            }}/>
                            <div className="m-1 text-center flex flex-row justify-self-center lg:text-sm xs:text-xxs items-center cursor-pointer bg-gray text-white justify-center rounded-lg hover:bg-black"
                            onClick={() => generatePassword()}>
                                <p>Wygeneruj hasło</p>
                            </div>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Telefon</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.phoneNumber} onChange={(e)=>{
                                var editedUser={...user,phoneNumber:e.target.value}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Email</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.email} onChange={(e)=>{
                                var editedUser={...user,email:e.target.value}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Login</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.login} onChange={(e)=>{
                                var editedUser={...user,login:e.target.value}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Stanowisko</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.department} onChange={(e)=>{
                                var editedUser={...user,department:e.target.value}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Płeć</p>
                            <select className="w-100p pl-2 rounded-lg focus:outline-none" onChange={(e)=>{
                                var editedUser={...user,gender:e.target.value}
                                setUser(editedUser)
                            }}>
                                <option value="false" selected={!user.gender}>Mężczyzna</option>
                                <option value="true" selected={user.gender}>Kobieta</option>
                            </select>
                        </div>
                    </div>
                    <div className="flex md:flex-row xs:flex-col w-100p xl:h-28 xs:h-36 md:space-y-0 xs:space-y-6 md:space-x-4 xs:space-x-0 border-1 md:p-5 xs:p-3 overflow-y-scroll overscroll-contain scrollbar-hide">
                        <div className="flex flex-col md:h-10 xs:h-30p md:w-15p xs:w-100p">
                            <p>Kod pocztowy</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.address.postCode} onChange={(e)=>{
                                var editedUser={...user,address:{...user.address,postCode:e.target.value}}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Miasto</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.address.city} onChange={(e)=>{
                                var editedUser={...user,address:{...user.address,city:e.target.value}}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Ulica</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.address.street} onChange={(e)=>{
                                var editedUser={...user,address:{...user.address,street:e.target.value}}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Numer domu</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.address.houseNumber} onChange={(e)=>{
                                var editedUser={...user,address:{...user.address,houseNumber:e.target.value}}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Numer lokalu</p>
                            <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={user.address.apartmentNumber} onChange={(e)=>{
                                var editedUser={...user,address:{...user.address,apartmentNumber:e.target.value}}
                                setUser(editedUser)
                            }}/>
                        </div>
                        <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                            <p>Województwo</p>
                            <select className="w-100p pl-2 rounded-lg focus:outline-none" onChange={(e)=>{
                                var editedUser={...user,address:{...user.address,province:e.target.value}}
                                setUser(editedUser)
                            }}>
                            {user.address.province!==''?null:<option selected disabled></option>}
                            {provinces.map((d,key)=>{
                              if(d===user.address.province){
                                return <option selected>{d}</option>
                              }else{
                                return <option>{d}</option>
                              }
                            })}
                            </select>
                        </div>
                    </div>
                    <div className="flex flex-col w-100p h-50p justify-start border-1 md:p-5 xs:p-3 overflow-y-scroll overscroll-contain scrollbar-hide">
                        <label>Uprawnienia</label>
                        <div className="w-100p h-100p flex md:flex-row md:flex-wrap xs:flex-col md:space-x-3 md:space-y-0 xs:space-x-0 xs:space-y-2 overflow-y-scroll overscroll-contain scrollbar-hide">
                            {user.permissions.map((perm) => (
                                <div className="h-10 flex flex-row space-x-2 items-center justify-center">
                                    <input type="checkbox" defaultChecked={perm.selected} chec></input>
                                    <label>{perm.name}</label>
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="flex flex-row w-100p md:h-8 xs:h-4 justify-around py-1">
                      <div className="h-100p w-30p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow p-2 cursor-pointer"
                       onClick={()=>handleUserSave()}>
                        Zapisz
                      </div>
                      <Link href="../administration">
                        <div className="h-100p w-30p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow p-2 cursor-pointer">
                          Anuluj
                        </div>
                      </Link>
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
      const res = await fetch(server+"Administration/GetUserData?userId="+context.params.userId, {
            method: 'GET',
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

  export default oppoData;