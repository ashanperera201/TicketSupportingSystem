import { createSlice } from '@reduxjs/toolkit'

export const projectSlice = createSlice({
    name: 'projects',
    initialState: {
        projects: undefined
    },
    reducers: {
        storeProjects: (state, action) => {
            state.projects = action.payload
        }
    }
});

export const { storeProjects } = projectSlice.actions;

export default projectSlice.reducer;