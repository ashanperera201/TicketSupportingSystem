import { createSelector } from 'reselect';

const selectProjects = (state: any) => state?.projectRef?.projects;


export const projectsSelector = createSelector([selectProjects], project => project);
