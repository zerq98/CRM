import Layout from "../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'
import {server} from '../server'

function productList(data) {
  const [session, loading] = useSession()
  const [isOpen, setIsOpen] = useState(false)
  const [selectedProduct,setSelectedProduct] = useState(
    {
      "id": 0,
      "name": "",
      "vatRate":1.0,
      "unitValue":1.0,
      "markupRate":1.0,
      "unitOfMeasurement":""
    }
  )
  var filters={
    'name': '',
    'vatRate': '',
    'markupRate':''
  }
  const [productList, setProductList] = useState([
    {
      "id": 0,
      "name": "",
      "vatRate":1.0,
      "unitValue":1.0,
      "markupRate":1.0,
      "unitOfMeasurement":""
    }
  ]);

  async function removeProduct(id){
    
    const res = await fetch(server+"Product/Remove?productId=" + id, {
      method: 'DELETE',
      body: {},
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + session.accessToken
      }
    })
    const resData = await res.json()

    if(resData.code === 200){
      setProductList(productList.filter(function (product){
        return product.id!==id
      }))
    }else{
      alert(resData.errorMessage)
    }
  }

  function changeTrashSet(e) {
    e.target.className = "bx bx-trash"
  }
  function changeTrashUnset(e) {
    e.target.className = "bx bxs-trash"
  }

  const handleFiltersChange = async function (val,filter){
    switch(filter){
      case 'name':
        filters.name=val
        break;
      case 'vatRate':
        filters.vatRate=val
        break;
      case 'markupRate':
        filters.markupRate=val
        break;
    }
    
    const res = await fetch(server+"Product/GetAll", {
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
        setProductList(resData.data)
      }else{
        alert(resData.errorMessage)
      }
  }

  useEffect(() => {
    if (data !== undefined) {
      setProductList(data.data)
    }
  }, [])

  function closeModal() {
    setIsOpen(false)
  }

  function openModal() {
    setIsOpen(true)
  }

  async function handleProductAdd(event) {
    event.preventDefault();
    event.stopPropagation();

    const product = {
      id:selectedProduct.id,
      name:event.target.name.value,
      vatRate: event.target.vatRate.value/100,
      markupRate: event.target.markupRate.value/100,
      unitValue:event.target.unitValue.value,
      unitOfMeasurement: event.target.unitOfMeasurement.value
    }

    const res = await fetch(product.id===0?server+"Product/Add":server+"Product/Edit", {
      method: product.id===0?'POST':'PATCH',
      body: JSON.stringify(product),
      headers: {
        accept: '*/*',
        "Content-Type": "application/json",
        "Authorization": "Bearer " + session.accessToken
      }
    })

    const resData = await res.json()

    if (resData.code === 201 || resData.code===200) {
      const res = await fetch(server+"Product/GetAll", {
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
        setProductList(resData.data)
      }
      product.id===0?alert('Dodano nowy produkt'):alert('Zaktualizowano pomyślnie')
      closeModal();
    } else {
      alert(resData.errorMessage)
    }
  }

    return(
        <Layout>
            <div className="flex flex-col w-100p h-100p items-center bg-layoutBG p-2 space-y-2 lg:text-lg xs:text-xs">
                <div className="flex flex-row w-100p h-15p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-1 xs:flex-wrap lg:flex-nowrap">
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center rounded-lg">
                    <p>Nazwa</p>
                    <input className="focus:outline-none pl-2 w-80p" type="text" onChange={(e)=>handleFiltersChange(e.target.value,'name')}></input>
                  </div>
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center rounded-lg">
                    <p>Procent VAT</p>
                    <input className="focus:outline-none pl-2 w-80p" type="text" onChange={(e)=>handleFiltersChange(e.target.value,'vatRate')}></input>
                  </div>
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center rounded-lg">
                    <p>Marża</p>
                    <input className="focus:outline-none pl-2 w-80p" type="text" onChange={(e)=>handleFiltersChange(e.target.value,'markupRate')}></input>
                  </div>
                  <div className="flex flex-col h-100p xs:w-25p lg:w-20p justify-start items-center cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white rounded-lg"
                  onClick={() => {
                    setSelectedProduct(
                      {
                        "id": 0,
                        "name": "",
                        "vatRate":0,
                        "unitValue":0,
                        "markupRate":0,
                        "unitOfMeasurement":""
                      }
                    )
                    openModal();
                  } }>
                      <i className='bx bxs-add-to-queue'></i>
                      <div className="xs:hidden md:block">
                        Nowy produkt
                      </div>
                      <div className="xs:block md:hidden">
                        Dodaj
                      </div>
                    </div>
                </div>
                <div className="flex flex-row w-100p h-10p bg-opacity-75 backdrop-filter backdrop-blur-lg bg-white border-b-1 border-black rounded-lg items-center text-center xs:flex-wrap lg:flex-nowrap">
                      <div className="border-r-1 border-black w-14">ID</div>
                      <div className="border-r-1 border-black xs:w-28 md:w-28 lg:w-48">Nazwa produktu</div>
                      <div className="border-r-1 border-black w-32">Procent VAT</div>
                      <div className="border-r-1 border-black xs:w-20 md:w-28 lg:w-40">Marża</div>
                      <div className="xs:border-r-0 lg:border-r-1 border-black xs:w-20 md:w-48">Cena jednostkowa</div>
                      <div className="border-r-1 border-black w-52 xs:hidden lg:block">Jednostka miary</div>
                      <div className="flex-grow"></div>
                </div>
                <div className="w-100p max-h-100p h-100p flex-grow rounded-lg bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg flex flex-col flex-nowrap items-center overflow-y-scroll overscroll-contain scrollbar-hide xs:space-y-4 md:space-y-2">
                    {productList.map((d, idx) => (
                        <div key={d.id} className='group flex flex-row w-100p h-10 py-2 border-black rounded-lg items-center text-center cursor-pointer hover:bg-opacity-45 hover:backdrop-filter hover:backdrop-blur-lg hover:bg-white xs:flex-wrap lg:flex-nowrap'
                        >
                            <div className="flex flex-row h-100p w-90p cursor-pointer" 
                              onClick={() => {
                                setSelectedProduct(d)
                                openModal();
                              } }>
                              <div className="border-r-1 border-black w-14">{d.id}</div>
                              <div className="border-r-1 border-black xs:w-28 md:w-28 lg:w-48">{d.name}</div>
                              <div className="border-r-1 border-black w-32">{d.vatRate*100}%</div>
                              <div className="border-r-1 border-black xs:w-20 md:w-28 lg:w-40">{d.markupRate*100}%</div>
                              <div className="xs:border-r-0 lg:border-r-1 border-black xs:w-20 md:w-48">{d.unitValue}</div>
                              <div className="border-r-1 border-black w-52 xs:hidden lg:block">{d.unitOfMeasurement}</div>
                            </div>
                            <div className="flex-grow">
                              <i className='bx bxs-trash' onClick={() => removeProduct(d.id)} onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset}></i>
                            </div>
                        </div>
                      ))}
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
                          {selectedProduct.id===0?
                          <Dialog.Title
                            as="h3"
                            className="text-lg font-medium leading-6 text-white"
                          >
                            Nowy produkt
                          </Dialog.Title>:
                          <Dialog.Title
                            as="h3"
                            className="text-lg font-medium leading-6 text-white"
                          >
                            Produkt
                          </Dialog.Title>}
                          <div className="mt-2">
                            <form className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p text-white" onSubmit={event => handleProductAdd(event)}>
                              <div className="flex w-100p flex-col">
                                <p>Nazwa produktu</p>
                                <input id="name" name="name" type="text" required defaultValue={selectedProduct.name}
                                  className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                              </div>
                              <div className="flex w-100p flex-col">
                                <p>Vat</p>
                                <div className="w-100p flex flex-row xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 space-x-2">
                                  <input id="vatRate" name="vatRate" type="text" required defaultValue={selectedProduct.vatRate*100}
                                    className="shadow-inner w-50p h-100p text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                                  <p className="h-100p">%</p>
                                </div>
                              </div>
                              <div className="flex w-100p flex-col">
                                <p>Marża</p>
                                <div className="w-100p flex flex-row xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 space-x-2">
                                  <input id="markupRate" name="markupRate" type="text" required defaultValue={selectedProduct.markupRate*100}
                                    className="shadow-inner w-50p h-100p text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                                  <p className="h-100p">%</p>
                                </div>
                              </div>
                              <div className="flex w-100p flex-col">
                                <p>Cena jednostkowa (w zł)</p>
                                <input id="unitValue" name="unitValue" type="text" required defaultValue={selectedProduct.unitValue}
                                  className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                              </div>
                              <div className="flex w-100p flex-col">
                                <p>Jednostka miary (skrót)</p>
                                <input id="unitOfMeasurement" name="unitOfMeasurement" type="text" required defaultValue={selectedProduct.unitOfMeasurement}
                                  className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5" />
                              </div>
                              {selectedProduct.id===0?
                              <button className='bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p'
                                type='submit'>
                                Dodaj produkt
                              </button>:
                              <button className='bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p'
                                type='submit'>
                                Aktualizuj
                              </button>}
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
      const res = await fetch(server+"Product/GetAll", {
        method: 'POST',
        body:JSON.stringify({
          'name': '',
          'vatRate': '',
          'markupRate':''
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

  export default productList;