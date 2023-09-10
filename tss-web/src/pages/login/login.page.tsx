import React, { useState, useEffect } from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';


import { authLogin, getUser } from '../../shared/services/auth.service';
import { IAuthLogin } from '../../shared/interfaces/auth-login.interface';
import { storeUser } from '../../redux/slices/userSlice';
import './login.scss';
import { setAccessToken, decodeAccessToken } from '../../shared/services/token.service';


const LoginPage = (): JSX.Element => {

    const navigate = useNavigate();
    const dispatch = useDispatch()

    const [userName, setUserName] = useState<string>('');
    const [password, setPassword] = useState<string>('');


    useEffect(() => {


        return () => {
            setUserName('');
            setPassword('');
        }
    }, [])

    const onLoginClick = async (): Promise<void> => {
        if (userName && password) {

            const payload: IAuthLogin = {
                userName: userName,
                password: password
            }

            try {
                const authRes = (await authLogin(payload))?.data;
                if (authRes) {
                    setAccessToken(authRes.accessToken);
                    const tokenInformation = decodeAccessToken();
                    if (tokenInformation) {
                        const user = (await getUser(tokenInformation.id))?.data?.user;
                        if (user) {
                            dispatch(storeUser(user));
                            navigate('/project');
                        }
                    }
                }
            } catch (error) {
                console.log(error);
            }
        } else {
            console.log('user name & password required.');
        }
    }

    return (
        <>
            <div className='login-container'>
                <div className="login-container__background"></div>
                <div className="login-container__login">
                    <TextField
                        id="userName"
                        label="User Name"
                        variant="outlined"
                        fullWidth
                        required
                        onChange={(e) => setUserName(e.target.value)}
                    />

                    <TextField
                        id="userName"
                        label="Password"
                        variant="outlined"
                        type='password'
                        fullWidth
                        style={{ marginTop: 30 }}
                        onChange={(e) => setPassword(e.target.value)}
                    />

                    <Button variant="contained" style={{ marginTop: 10 }} fullWidth onClick={onLoginClick} disabled={!(userName && password)}>Login</Button>
                </div>
            </div>
        </>
    )
}



export default LoginPage;