import Layout from "../../components/layout";
import React, { useState, useEffect, Fragment } from 'react';
import { getSession, options, useSession } from "next-auth/client";
import { Tab, Dialog, Transition } from '@headlessui/react'
import Link from 'next/link'
import { useRouter } from 'next/router'
import {server} from '../../server'

function oppoData(data) {
  const [session, loading] = useSession()
  const router = useRouter()
  const[isOpen,setIsOpen] = useState(false);
  const[quantity,setQuantity] = useState(0);
  const[selectedProduct,setSelectedProduct]=useState(
    {
    "name":"",
    "unitOfMeasurement":"",
    "unitValue":0,
    "markupRate":0,
    "vatRate":0
    }
  );
  const statuses=[
    'Nowa',
    'Modyfikowana',
    'Anulowana',
    'Zaakceptowana',
    'Oferta'
  ]
  const [oppo, setOppo] = useState(
    {
        "id": 0,
        "lead": "",
        "trader": "",
        "status": "",
        "positions":[
          {
            "id":0,
            "product":"",
            "quantity":0,
            "netValue": 0,
            "grossValue": 0,
            "markup": 0,
            "vatValue": 0,
            "unitOfMeasurement": "",
            "deleted":false,
            "localId":0
          }
        ]
      }
  );
  const [tradersList, setTradersList] = useState([
    "x"
  ]);
  const [leadList, setLeadList] = useState([
    "x"
  ]);
  const [productList, setProductList] = useState([
    {
        "name":"",
        "unitOfMeasurement":0,
        "unitValue":0,
        "markupRate":0,
        "vatRate":0
    }
  ]);

  function closeModal() {
    setIsOpen(false)
  }

  function openModal() {
    if(productList.length>0 && productList[0].name!==""){
      setIsOpen(true)
    }else{
      alert("Brak produktów w kartotece")
    }
  }

  function handlePositionDelete(id){
    if(id!==undefined){
      var editedOppo = {...oppo,positions:oppo.positions.map((pos) => {
        if (pos.localId === id ) {
          return {
            ...pos,
            deleted:true
          }
        }
        return pos
      })}
  
      setOppo(editedOppo)
    }
  }

  function handlePositionAdd(){

    var position = {
      id:0,
      quantity:quantity,
      product:selectedProduct.name,
      netValue:selectedProduct.unitValue*quantity,
      grossValue:(selectedProduct.unitValue*quantity)+(selectedProduct.unitValue*quantity*selectedProduct.vatRate),
      vatValue:selectedProduct.unitValue*quantity*selectedProduct.vatRate,
      markup:selectedProduct.unitValue*quantity*selectedProduct.markupRate,
      unitOfMeasurement:selectedProduct.unitOfMeasurement
    }

    if(position.product!='' && position.product!='Produkt' && position.quantity!==0){
      var editedOppo = {
        ...oppo
        ,positions:[...oppo.positions,{
        id:0,
        product:position.product,
        quantity:position.quantity,
        netValue: position.netValue,
        grossValue: position.grossValue,
        markup: position.markup,
        vatValue: position.vatValue,
        unitOfMeasurement: position.unitOfMeasurement,
        deleted:false,
        localId:oppo.positions.length+1
      }]}
  
      setOppo(editedOppo)
    }
    setSelectedProduct({
      "name":"",
      "unitOfMeasurement":0,
      "unitValue":0,
      "markupRate":0,
      "vatRate":0
    });
    closeModal();
  }

  function findProductIndex(element){
    return element.name===this
  }

  function validate(){
    var isOk=true;

    if(oppo.status==='' || oppo.status===undefined){
      isOk=false
    }

    if(oppo.trader==='' || oppo.trader===undefined){
      isOk=false
    }

    if(oppo.lead==='' || oppo.lead===undefined){
      isOk=false
    }

    if(oppo.positions.length===0 || oppo.positions===undefined){
      isOk=false
    }

    return isOk;
  }

  const handleOppoSave = async function(){
    if (validate()) {
      const res = await fetch(server+"Opportunity/UpsertOpportunity", {
          method: 'POST',
          body: JSON.stringify(oppo),
          headers: {
              accept: '*/*',
              "Content-Type": "application/json",
              "Authorization": "Bearer " + session.accessToken
          }
      })

      const resData = await res.json()

      if (resData.code === 201) {
          router.push('/orders')
      } else {
          alert(resData.errorMessage)
          return 'Wrong data';
      }
  }
  }

  useEffect(() => {
    if (data !== undefined ) {
      setOppo(data.data.sellOpportunity)
      setTradersList(data.data.companyTraders)
      setProductList(data.data.products)
      setLeadList(data.data.leads)
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
                <div className="flex flex-col w-100p h-100p space-y-2 overflow-y-scroll overscroll-contain scrollbar-hide">
                  <div className="flex flex-col w-100p h-20p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg p-2 md:space-y-2 ">
                    <div className="flex md:flex-row xs:flex-col border-1 h-80p w-100p p-2 overflow-y-scroll overscroll-contain scrollbar-hide md:space-x-2">
                        <div className="flex flex-col md:w-30p xs:w-100p h-100p space-y-0.5">
                          <p>Lead</p>
                          <p>{oppo.lead}</p>
                        </div>
                        <div className="flex flex-col md:w-30p xs:w-100p h-20p space-y-0.5">
                          <p>Status</p>
                          <p>{oppo.status}</p>
                        </div>
                        <div className="flex flex-col md:w-30p xs:w-100p h-20p space-y-0.5">
                          <p>Handlowiec</p>
                          <p>{oppo.trader}</p>
                        </div>
                      </div>
                    <div className="flex flex-row w-100p h-20p justify-around">
                      <div className="h-100p w-10p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow xs:p-4 md:p-3 cursor-pointer"
                      onClick={handleOppoSave}>
                        Zapisz
                      </div>
                      <Link href="../orders">
                        <div className="h-100p w-10p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow xs:p-4 md:p-3 cursor-pointer">
                          Anuluj
                        </div>
                      </Link>
                    </div>
                  </div>
                  <div className="flex flex-col w-100p h-100p space-y-2">
                    <div className="flex flex-col w-100p h-100p bg-opacity-35 backdrop-filter backdrop-blur-lg bg-white rounded-lg space-y-2 p-2">
                      <div className="flex flex-row w-100p h-10p justify-start items-center space-x-4">
                        <p>Pozycje szansy</p>
                        <div className=" h-100p text-center flex flex-row justify-center items-center bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 font-bold text-white shadow p-2 cursor-pointer"
                        onClick={openModal}>
                          Dodaj pozycje
                        </div>
                      </div>
                      <div className='group rounded-lg w-100p px-2 h-10 items-center py-2 flex flex-row space-x-2 bg-opacity-35 backdrop-filter backdrop-blur-lg bg-gray rounded-lg '>
                          <div className="w-100p flex flex-row justify-start space-x-3">
                            <div className="xs:w-5 lg:w-10 truncate">Id</div>
                            <div className="xs:w-28 lg:w-48 truncate">Nazwa produktu</div>
                            <div className="xs:w-8 lg:w-16 truncate">Ilość</div>
                            <div className="xs:w-16 lg:w-28 truncate">Jednostka</div>
                            <div className="xs:w-12 lg:w-24 truncate">Wartość</div>
                          </div>
                      </div>
                      <div className="w-100p h-90p border-1 bg-white rounded-lg flex flex-col flex-nowrap items-center overflow-y-scroll overscroll-contain scrollbar-hide p-1">
                      {oppo.positions.map((d, idx) => (
                        !d.deleted &&
                        <div key={d.localId} className='group rounded-lg w-100p px-2 h-16 items-center py-2 flex flex-row space-x-2 cursor-pointer bg-opacity-35 backdrop-filter backdrop-blur-lg bg-gray rounded-lg '>
                          <div className="w-100p flex flex-row justify-start space-x-3">
                            <div className="xs:w-5 lg:w-10 truncate">{d.localId}</div>
                            <div className="xs:w-28 lg:w-48 truncate">{d.product}</div>
                            <div className="xs:w-8 lg:w-16 truncate">{d.quantity}</div>
                            <div className="xs:w-16 lg:w-28 truncate">{d.unitOfMeasurement}</div>
                            <div className="xs:w-12 lg:w-24 truncate">{d.grossValue}</div>
                          </div>
                          <i className='bx bxs-trash xs:w-5 lg:w-10' onMouseOver={changeTrashSet} onMouseLeave={changeTrashUnset} onClick={() => handlePositionDelete(d.localId)}></i>
                        </div>
                      ))}
                      </div>
                    </div>
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
                            Nowa pozycja
                          </Dialog.Title>
                          <div className="mt-2">
                            <div className="flex flex-col xs:space-y-5 lg:space-y-10 items-center justify-center h-100p w-100p">
                              <input onChange={(e)=>{
                                setQuantity(e.target.value)
                              }} placeholder="Ilość"
                              className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5">
                                </input>
                              <select onChange={(e)=>{
                                setSelectedProduct(productList[productList.findIndex(findProductIndex,e.target.value)])
                              }}
                                className="shadow-inner w-100p xs:text-sm sm:text-lg md:text-xl xs:h-7 sm:h-8 md:h-9 text-black rounded-lg focus:outline-none xs:pl-2 sm:pl-3 lg:pl-5">
                                  <option selected disabled>Produkt</option>
                                  {productList.map((d,key)=>{
                                      return <option>{d.name}</option>
                                  })}
                                  </select>
                              <div className='cursor-pointer bg-gray rounded-xl hover:bg-white hover:text-gray duration-300 xs:text-sm sm:text-lg md:text-xl font-bold text-white shadow p-2 md:w-70p xs:w-80p'
                                onClick={handlePositionAdd}>
                                Dodaj pozycję
                              </div>
                            </div>
                          </div>
                        </div>
                      </Transition.Child>
                    </div>
                  </Dialog>
                </Transition>
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
      const res = await fetch(server+"Opportunity/GetOpportunity?opportunityId="+context.params.orderId, {
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