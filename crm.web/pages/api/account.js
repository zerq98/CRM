import axios from 'axios';
import ApiRestService from './ApiRestService';

export const logIn = async (login,password)=>{
    const httpService = new ApiRestService();
    process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
    httpService.getInstance().post('api/Account/Login',
            JSON.stringify({
                    login: login,
                    password: password
                }
            )
        ).then(response => {
            if (typeof response.data !== "object"){
                alert('Sprawdź ponownie dane logowania')
                return 'Wrong data';
            }

            alert('Zalogowano');
        }).catch(error =>{
            if(error.response.status === 401){
                alert('Sprawdź ponownie dane logowania')
                return 'Wrong data';
            }
        })
}