import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, useSession } from "next-auth/client";
import { Tab } from '@headlessui/react'
import { server } from '../server'
import Link from 'next/link'
import { Bar, Doughnut } from 'react-chartjs-2';

function todoList(data) {
  const [session, loading] = useSession()
  const [isOpen, setIsOpen] = useState(false)
  const [companyData, setCompanyData] = useState({
    "id": 0,
    "name": "",
    "nip": "",
    "regon": "",
    "address": {
      "postCode": "",
      "city": "",
      "houseNumber": "",
      "apartmentNumber": "",
      "street": "",
      "province": ""
    }
  })
  const [salesData, setSalesData] = useState([
    0,
    0,
    0,
    0,
    0
  ])
  const [activityData, setActivityData] = useState([
    0,
    0,
    0,
    0,
    0,
    0
  ])
  const [thisMonthData, setThisMonthData] = useState([
    0,
    0,
    0
  ])
  const [thisYearData, setThisYearData] = useState([
    0,
    0,
    0
  ])
  const [selectedUser, setSelectedUser] = useState({
    "id": 0,
    "name": "",
    "department": "",
    "gender": false,
    "startDate": "",
    "canDelete": false
  })
  const [userList, setUserList] = useState([
    {
      "id": 0,
      "name": "",
      "department": "",
      "gender": false,
      "startDate": "",
      "canDelete": false
    }
  ]);

  useEffect(() => {
    if (data !== undefined && data.data !== null) {
      setUserList(data.data.users)
      setSalesData(data.data.statistics.opportunities)
      setActivityData(data.data.statistics.activities)
      var thisMonth = [
        data.data.statistics.thisMonthNet,
        data.data.statistics.thisMonthGross,
        data.data.statistics.thisMonthMarkup,
      ]
      setThisMonthData(thisMonth)
      var thisYear = [
        data.data.statistics.thisYearNet,
        data.data.statistics.thisYearGross,
        data.data.statistics.thisYearMarkup,
      ]
      setThisYearData(thisYear)
      setCompanyData(data.data.company)
    }
  }, [])

  function changeTrashSet(e) {
    e.target.className = "bx bx-trash"
  }
  function changeTrashUnset(e) {
    e.target.className = "bx bxs-trash"
  }

  const saleChances = {
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

  const thisMonthSales = {
    labels: ['Netto', 'Brutto', 'Marża'],
    datasets: [{
      label: 'Suma szans z tego miesiąca',
      data: thisMonthData,
      backgroundColor: [
        'rgba(0, 0, 255, 1)',
        'rgba(255, 69, 0, 1)',
        'rgba(255, 255, 0, 1)',
      ],
      borderWidth: 0,
    }]
  }

  const thisYearSales = {
    labels: ['Netto', 'Brutto', 'Marża'],
    datasets: [{
      label: 'Suma szans z tego roku',
      data: thisYearData,
      backgroundColor: [
        'rgba(0, 0, 255, 1)',
        'rgba(255, 69, 0, 1)',
        'rgba(255, 255, 0, 1)',
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
      hoverOffset: [
        20, 20, 20, 20, 20, 20
      ],
      offset: [
        10, 10, 10, 10, 10, 10
      ]
    }]
  };
  const provinces = [
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

  function changeTrashSet(e) {
    e.target.className = "bx bx-trash"
  }
  function changeTrashUnset(e) {
    e.target.className = "bx bxs-trash"
  }

  async function removeUser(id){
    const res = await fetch(server+"Administration/RemoveUser?userId=" + id, {
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
      setUserList(userList.filter(function (user){
        return user.id!==id
      }))
    }else{
      alert('Użytkownik ma przypisane szanse sprzedaży. \r\nZmień handlowca na szansach tego użytkownika, żeby móc go usunąć.')
    }
  }

  async function handleDataSave() {
    const res = await fetch(server + "Administration/UpdateCompanyData", {
      method: 'POST',
      body: JSON.stringify(companyData),
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + session.accessToken
      }
    })

    const resData = await res.json()

    if (resData.code === 201) {
      alert('Dane zaktualizowane pomyślnie.')
    } else {
      alert(resData.errorMessage)
    }
  }

  return (
    <Layout>
      <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-2 space-y-2 xs:text-sm md:text-lg lg:text-2xl">
        <Tab.Group>
          <Tab.List className="w-100p xs:h-15p md:h-20 bg-opacity-35 p-2 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex xs:flex-col md:flex-row items-center xs:space-y-2 md:space-x-2 xs:space-x-0 md:space-y-0 overflow-y-auto overscroll-contain scrollbar-hide">
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
            <div className="hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg h-100p w-100p cursor-pointer">
                <Link href="C:/CRM_Desktop.zip" download>
                  Pobierz na desktop
                </Link>
              </div>
          </Tab.List>
          <Tab.Panels className="md:h-100p xs:h-85p w-100p">
            <Tab.Panel className="bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg w-100p h-100p scrollbar-hide overflow-auto overscroll-y-contain">
              <div className="w-100p xs:h-auto md:h-100p flex xs:flex-col md:flex-row md:justify-between xs:flex-nowrap md:flex-wrap p-2 xs:space-y-4 md:space-y-0">
                <div className="xs:w-100p xs:h-64 md:w-45p md:h-45p ">
                  <Bar
                    data={saleChances}
                    width='100%'
                    height='100%'
                    className="border-1 rounded-lg p-1"
                    options={{
                      maintainAspectRatio: false,
                      plugins: {
                        legend: {
                          display: true,
                          labels: {
                            boxWidth: 0,
                            boxHeight: 0,
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
                          ticks: {
                            display: false
                          }
                        }
                      },
                      responsive: true
                    }}
                  />
                </div>
                <div className="xs:w-100p xs:h-64 md:w-45p md:h-45p">
                  <Doughnut
                    data={contactTypes}
                    width='100%'
                    height='100%'
                    className="border-1 rounded-lg p-1"
                    options={{
                      maintainAspectRatio: false,
                    }}
                  />
                </div>
                <div className="xs:w-100p xs:h-64 md:w-45p md:h-45p">
                  <Bar
                    data={thisMonthSales}
                    width='100%'
                    height='100%'
                    className="border-1 rounded-lg p-1"
                    options={{
                      maintainAspectRatio: false,
                      plugins: {
                        legend: {
                          display: true,
                          labels: {
                            boxWidth: 0,
                            boxHeight: 0,
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
                          ticks: {
                            display: false
                          }
                        }
                      },
                      responsive: true
                    }}
                  />
                </div>
                <div className="xs:w-100p xs:h-64 md:w-45p md:h-45p">
                  <Bar
                    data={thisYearSales}
                    width='100%'
                    height='100%'
                    className="border-1 rounded-lg p-1"
                    options={{
                      maintainAspectRatio: false,
                      plugins: {
                        legend: {
                          display: true,
                          labels: {
                            boxWidth: 0,
                            boxHeight: 0,
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
                          ticks: {
                            display: false
                          }
                        }
                      },
                      responsive: true
                    }}
                  />
                </div>
              </div>
            </Tab.Panel>
            <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center p-2 scrollbar-hide overflow-auto overscroll-contain space-y-2">
              {userList.map((user) => (
                <div className="flex flex-row w-100p items-center cursor-pointer">
                <Link href={"/user/" + user.id}>
                  <div className="flex flex-row w-100p items-center space-x-4 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg">
                    {user.gender ? <i class='bx bx-female'></i> : <i class='bx bx-male'></i>}
                    <div className="flex flex-col">
                      <div>{user.name}</div>
                      <div className="flex flex-row space-x-2">
                        <div className="xs:text-xxs md:text-sm lg:text-md">{user.department}</div>
                        <div className="xs:text-xxs md:text-sm lg:text-md">Rozpoczęcie: {user.startDate}</div>
                      </div>
                    </div>
                  </div>
                </Link>
                {user.canDelete? <i className='bx bxs-trash' onClick={() => removeUser(user.id)} onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset}></i> : <div></div>}
                </div>
              ))}
              <Link href={"/user/0"}>
                <div className="flex flex-row w-100p items-center space-x-4 cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg">
                  <i class='bx bx-plus'></i>
                  <div>Dodaj użytkownika</div>
                </div>
              </Link>
            </Tab.Panel>
            <Tab.Panel className="w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center pt-2 overflow-auto overscroll-contain scrollbar-hide xs:space-y-4 md:space-y-0">
              <div className="w-100p h-100p items-center bg-layoutBG p-7 lg:text-lg xs:text-xs overflow-y-scroll overscroll-contain scrollbar-hide">
                <div className="flex flex-col items-center space-y-2 w-100p h-100p items-center p-2 bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg">
                  <div className="flex md:flex-row xs:flex-col w-100p xl:h-28 xs:h-36 md:space-y-0 xs:space-y-6 md:space-x-4 xs:space-x-0 border-1 p-3 overflow-y-scroll overscroll-contain scrollbar-hide">
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-30p xs:w-100p">
                      <p>Nazwa firmy</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.name} onChange={(e) => {
                        var editedData = { ...companyData, name: e.target.value }
                        setCompanyData(editedData)
                      }} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-30p xs:w-100p">
                      <p>NIP</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.nip} onChange={(e) => {
                        var editedData = { ...companyData, nip: e.target.value }
                        setCompanyData(editedData)
                      }} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-30p xs:w-100p">
                      <p>Regon</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.regon} onChange={(e) => {
                        var editedData = { ...companyData, regon: e.target.value }
                        setCompanyData(editedData)
                      }} />
                    </div>
                  </div>
                  <div className="flex md:flex-row xs:flex-col w-100p xl:h-28 xs:h-36 md:space-y-0 xs:space-y-6 md:space-x-4 xs:space-x-0 border-1 md:p-5 xs:p-3 overflow-y-scroll overscroll-contain scrollbar-hide">
                    <div className="flex flex-col md:h-10 xs:h-30p md:w-15p xs:w-100p">
                      <p>Kod pocztowy</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.address.postCode} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                      <p>Miasto</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.address.city} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                      <p>Ulica</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.address.street} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                      <p>Numer domu</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.address.houseNumber} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                      <p>Numer lokalu</p>
                      <input className="w-100p pl-2 rounded-lg focus:outline-none" defaultValue={companyData.address.apartmentNumber} />
                    </div>
                    <div className="flex flex-col md:h-10 xs:h-20p md:w-15p xs:w-100p">
                      <p>Województwo</p>
                      <select className="w-100p pl-2 rounded-lg focus:outline-none">
                        {companyData.address.province !== '' ? null : <option selected disabled></option>}
                        {provinces.map((d, key) => {
                          if (d === companyData.address.province) {
                            return <option selected>{d}</option>
                          } else {
                            return <option>{d}</option>
                          }
                        })}
                      </select>
                    </div>
                  </div>
                  <div className="flex flex-col w-100p h-50p justify-start md:p-5 xs:p-3 overflow-y-scroll overscroll-contain scrollbar-hide">
                  </div>
                  <div className="flex flex-row w-100p md:h-8 xs:h-4 justify-around py-1">
                    <div className="h-100p w-30p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow p-2 cursor-pointer"
                      onClick={() => handleDataSave()}>
                      Zapisz
                    </div>
                  </div>
                </div>
              </div>
            </Tab.Panel>
          </Tab.Panels>
        </Tab.Group>
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
    const res = await fetch(server + "Administration/GetCompanyData", {
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

export default todoList;