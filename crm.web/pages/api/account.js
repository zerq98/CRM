import ApiRestService from './ApiRestService';
import Router from 'next/router';
import { useEffect } from 'react';
import axios from 'axios'
import cookieCutter from 'cookie-cutter'

export const logIn = async (login,password)=>{
    const httpService = new ApiRestService();
    
    httpService.getInstance().post('api/Account/Login',
            JSON.stringify({
                    login: login,
                    password: password
                }
            )
        ).then(function (response){
            console.log(response);
            if(response.data.code===200){
                cookieCutter.set('userToken',response.data.data.token,{expires: new Date(response.data.data.expireDate)})
                cookieCutter.set('userId',response.data.data.id,{expires: new Date(response.data.data.expireDate)})
                cookieCutter.set('tokenExpiration',new Date(response.data.data.expireDate),{expires: new Date(response.data.data.expireDate)})
                Router.push('/dashboard')
            }
            if(response.data.code===401){
                alert(response.data.errorMessage)
                return 'Wrong data'
            }
            
        }).catch(function (error){
            alert('Nastąpił problem. Zgłoś się do działu IT.')
            return 'Wrong data'
            
        })
}

export const register = async (data)=>{
    const httpService = new ApiRestService();
    httpService.getInstance().post('api/Account/RegisterCompany',
            JSON.stringify({
                user: {
                    login: data.login,
                    password: data.password,
                    email: data.mail,
                    firstName: data.firstName,
                    lastName: data.lastName,
                    phoneNumber: data.phone
                  },
                  companyAddress: {
                    postCode: data.postCode,
                    city: data.city,
                    street: data.street,
                    houseNumber: data.houseNo,
                    apartmentNumber: data.flatNo,
                    province: data.province
                  },
                  userAddress: {
                    postCode: '',
                    city: '',
                    street: '',
                    houseNumber: '',
                    apartmentNumber: '',
                    province: ''
                  },
                  company: {
                    companyName: data.companyName,
                    nip: '',
                    regon: ''
                  }
                }
            )
        ).then(response => {
            if (response.data.code === 201){
                Router.push('/registerConfirmed')
            }
            if(response.data.code === 500){
                alert(response.data.errorMessage)
                return 'Wrong data';
            }
        }).catch(error =>{
            alert('Nastąpił problem. Zgłoś się do działu IT.')
            return 'Wrong data'
        })
}

export async function getDashboardData(userId,token){

    let headers = {
        'Content-Type': 'application/json',
        'Authorization': `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjllZWMzNWY0LTRiOTctNDBkZi04ZDYzLWM1NjE1NzNlNDc1MiIsIklUIEFkbWluaXN0cmF0b3IiOiJJVCBBZG1pbmlzdHJhdG9yIiwibmJmIjoxNjI5NTgzMTUxLCJleHAiOjE2Mjk2Njk1NTAsImlhdCI6MTYyOTU4MzE1MX0.zeg2IPil1KDVkdpk1oC0Hxgs0-JmCJEBX6AFhYA5w7k`
    }

    var ax = axios.create({
        baseURL: 'https://localhost:44395/',
        timeout: 60000,
        headers: headers
    })

    process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
    var data = {
        "name":"",
        "department": "",
        "position": ""
    }

    const res = await ax.get(`api/Account/DashboardInfo?id=${userId}`)
    
    data=await res
    console.log(data.data)
    return data.data.data
}