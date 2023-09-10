import { configureStore } from '@reduxjs/toolkit';

import userReducer from './slices/userSlice';
import projectReducer from './slices/projectSlice';

export default configureStore({
    reducer: {
        userRef: userReducer,
        projectRef: projectReducer
    }
})
