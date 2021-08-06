import ApiRestService from './ApiRestService';

export const logIn = async (login,password)=>{
    const httpService = new ApiRestService();
    httpService.getInstance().post('api/Account/Login',
            JSON.stringify({
                    login: login,
                    password: password
                }
            )
        ).then(function (response){
            if (typeof response.data !== object){
                alert('Sprawdź ponownie dane logowania')
                return 'Wrong data';
            }
            return{
                redirect: {
                    source: '/login',
                    destination: '/home',
                    permanent: true,
                  }
            }
        }).catch(function (error){
            console.log(error.response);
            if(error.response.status === 401){
                alert('Sprawdź ponownie dane logowania')
                return 'Wrong data';
            }
            if(error.response.status === 500){
                alert('Nastąpił problem z połączeniem z serwerem.\r\nSkontaktuj się z administracją')
                return 'Wrong data';
            }
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
            if (response.status === 201){
                return{
                    redirect: {
                        source: '/register',
                        destination: '/registered',
                        permanent: true,
                      }
                }
            }

            alert('Zalogowano');
        }).catch(error =>{
            if(error.response.status === 500){
                alert('Nastąpił problem z połączeniem z serwerem.\r\nSkontaktuj się z administracją')
                return 'Wrong data';
            }
        })
}