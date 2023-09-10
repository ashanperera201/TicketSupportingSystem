import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';


import { useDispatch } from 'react-redux'
import { fetchProjects } from '../../../shared/services/project.service';
import { storeProjects } from '../../../redux/slices/projectSlice';
import { projectsSelector } from '../../../redux/selectors/project.selector';

const Projects = (props: any): JSX.Element => {

    const { projects } = props;


    const dispatch = useDispatch();

    useEffect(() => {
        loadProjects();

        return () => {
        }
    }, []);

    const loadProjects = async (): Promise<void> => {
        try {
            const projects = (await fetchProjects())?.data
            if (projects) {
                dispatch(storeProjects(projects));
            }
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <>
            {
                projects && (
                    projects.map((x: any) => (
                        <h1>{x.projectName}</h1>
                    ))
                )
            }
        </>
    )
}

const mapStateToProps = createStructuredSelector({
    projects: projectsSelector,
})

export default connect(mapStateToProps, null)(Projects);
