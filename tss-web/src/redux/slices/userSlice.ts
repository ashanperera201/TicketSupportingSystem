import { createSlice } from '@reduxjs/toolkit'

export const userSlice = createSlice({
    name: 'user',
    initialState: {
        user: undefined
    },
    reducers: {
        storeUser: (state, action) => {
            state.user = action.payload
        }
    }
})

// Action creators are generated for each case reducer function
export const { storeUser } = userSlice.actions;

export default userSlice.reducer;