--
-- PostgreSQL database dump
--

-- Dumped from database version 14.13
-- Dumped by pg_dump version 14.13

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: adminpack; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS adminpack WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION adminpack; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION adminpack IS 'administrative functions for PostgreSQL';


--
-- Name: prevent_created_at_update(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.prevent_created_at_update() RETURNS trigger
    LANGUAGE plpgsql
    AS $$

BEGIN

    -- If created_at already has a value, prevent changes

    IF NEW.created_at IS DISTINCT FROM OLD.created_at THEN

        RAISE EXCEPTION 'Modification of created_at is not allowed';

    END IF;

    RETURN NEW;

END;

$$;


ALTER FUNCTION public.prevent_created_at_update() OWNER TO postgres;

--
-- Name: set_created_at_if_null(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.set_created_at_if_null() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Only set created_at if it is currently NULL
    IF NEW.created_at IS NULL AND OLD.created_at IS NULL THEN
        NEW.created_at := now();
    ELSIF NEW.created_at IS NULL AND OLD.created_at IS NOT NULL THEN
        NEW.created_at := OLD.created_at;
    END IF;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.set_created_at_if_null() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: tutorialgroup; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tutorialgroup (
    step_group_id integer NOT NULL,
    step_group_name character varying(255) NOT NULL,
    role_id integer[] NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_at timestamp without time zone,
    updated_at timestamp without time zone DEFAULT now(),
    step_group_description character varying(255)
);


ALTER TABLE public.tutorialgroup OWNER TO postgres;

--
-- Name: tutorialgroup_step_group_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tutorialgroup_step_group_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tutorialgroup_step_group_id_seq OWNER TO postgres;

--
-- Name: tutorialgroup_step_group_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tutorialgroup_step_group_id_seq OWNED BY public.tutorialgroup.step_group_id;


--
-- Name: tutorialstep; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tutorialstep (
    step_id integer NOT NULL,
    role_id integer,
    step_group_name character varying(255) NOT NULL,
    step_name character varying(255) NOT NULL,
    step_description text,
    step_page character varying(50) NOT NULL,
    step_order integer NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_at timestamp without time zone DEFAULT now(),
    updated_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.tutorialstep OWNER TO postgres;

--
-- Name: tutorialstep_step_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tutorialstep_step_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tutorialstep_step_id_seq OWNER TO postgres;

--
-- Name: tutorialstep_step_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tutorialstep_step_id_seq OWNED BY public.tutorialstep.step_id;


--
-- Name: usercompletedgroups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usercompletedgroups (
    user_id integer NOT NULL,
    step_group_name character varying(50) NOT NULL,
    completed_at timestamp without time zone DEFAULT now(),
    role_id integer NOT NULL
);


ALTER TABLE public.usercompletedgroups OWNER TO postgres;

--
-- Name: usercompletedsteps; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usercompletedsteps (
    user_id integer NOT NULL,
    step_id integer NOT NULL,
    completed_at timestamp without time zone DEFAULT now(),
    role_id integer NOT NULL
);


ALTER TABLE public.usercompletedsteps OWNER TO postgres;

--
-- Name: usertutorialprogress; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usertutorialprogress (
    user_id integer NOT NULL,
    role_id integer NOT NULL,
    current_step_group_name character varying(50),
    current_step_id integer,
    is_complete boolean DEFAULT false NOT NULL,
    created_at timestamp without time zone DEFAULT now(),
    updated_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.usertutorialprogress OWNER TO postgres;

--
-- Name: tutorialgroup step_group_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tutorialgroup ALTER COLUMN step_group_id SET DEFAULT nextval('public.tutorialgroup_step_group_id_seq'::regclass);


--
-- Name: tutorialstep step_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tutorialstep ALTER COLUMN step_id SET DEFAULT nextval('public.tutorialstep_step_id_seq'::regclass);


--
-- Data for Name: tutorialgroup; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tutorialgroup (step_group_id, step_group_name, role_id, is_active, created_at, updated_at, step_group_description) FROM stdin;
4	project_dashboard	{1,2,3,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	Project Dashboard
7	file_center	{1,2,3,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	File Center
2	create_plan	{1,2,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	Create Plan
9	invite_member	{1,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	Invite Member
6	comment	{1,2,3,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	Comment
3	upload_tour	{1,2,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	Upload Tour
8	report	{1,2,3,5}	t	2024-11-11 17:28:50.155905	2024-11-11 17:28:50.155905	Report
5	view_tour	{1,2,3,4,5}	t	2024-11-11 16:50:20.829656	2024-11-11 16:50:20.829656	View Tour
1	create_project	{1,5}	t	2024-11-11 17:22:09.562459	2024-11-11 17:22:09.562459	Create Project
\.


--
-- Data for Name: tutorialstep; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tutorialstep (step_id, role_id, step_group_name, step_name, step_description, step_page, step_order, is_active, created_at, updated_at) FROM stdin;
1	1	create_project	Create project	การสร้างโครงการ\\nคลิก Add Project เพื่อสร้างโครงการใหม่	home	1	t	2024-11-11 21:34:42.658058	2024-11-11 21:34:42.658058
2	1	create_project	Cover Photo	อัปโหลดภาพปกโครงการ\\nDownload ตัวอย่างไฟล์	home	2	t	2024-11-11 21:34:42.717422	2024-11-11 21:34:42.717422
3	1	create_project	Project Name , Project Code	กรอกชื่อโครงการ\\nCode โครงการ (จำกัด 10 อักษรหรือตัวเลข)	home	3	t	2024-11-11 21:34:42.740056	2024-11-11 21:34:42.740056
4	1	create_project	Project Type	เลือกประเภทโครงการ	home	4	t	2024-11-11 21:34:42.771664	2024-11-11 21:34:42.771664
5	1	create_project	Project Duration	ระยะเวลาโดยประมาณของโครงการ (วันที่เริ่ม - จบโครงการโดยประมาณ)	home	5	t	2024-11-11 21:34:42.788501	2024-11-11 21:34:42.788501
6	1	create_project	Save	บันทึก	home	6	t	2024-11-11 21:34:42.803744	2024-11-11 21:34:42.803744
7	1	create_plan	Upload Plan	คลิกจุด 3 จุดเข้าไปอัปโหลด Plan\\nคลิก Edit เพื่อเพิ่มหรือแก้ไขแปลน	home	7	t	2024-11-11 21:34:42.837688	2024-11-11 21:34:42.837688
8	1	create_plan	Upload Plan	คลิก New plan เพื่อเพิ่มแปลนใหม่	home	8	t	2024-11-11 21:34:42.861064	2024-11-11 21:34:42.861064
9	1	create_plan	Upload Plan	คลิกเพื่ออัปโหลดไฟล์ภาพแปลน (JPG, PNG, JPEG…)\\nDownload ตัวอย่างไฟล์	home	9	t	2024-11-11 21:34:42.886732	2024-11-11 21:34:42.886732
10	1	create_plan	Plan name / Plan Type	ชื่อแปลน /เลือกประเภทของแปลน	home	10	t	2024-11-11 21:34:42.901442	2024-11-11 21:34:42.901442
11	1	create_plan	Add another Plan	อัปโหลดแปลนอื่นๆ เพิ่มเติม	home	11	t	2024-11-11 21:34:42.919073	2024-11-11 21:34:42.919073
12	1	create_plan	Create	คลิกเพื่อสร้างแปลนใหม่	home	12	t	2024-11-11 21:34:42.937893	2024-11-11 21:34:42.937893
13	1	upload_tour	Add Tour	คลิก Add Tour เพื่อสร้างรายการ	home	13	t	2024-11-11 21:34:42.954288	2024-11-11 21:34:42.954288
14	1	upload_tour	Select 360 Image 	เลือก 360 Image	home	14	t	2024-11-11 21:34:42.968797	2024-11-11 21:34:42.968797
15	1	upload_tour	Upload 360 Image 	คลิกเพื่อเลือกไฟล์ภาพ 360 จากคอมพิวเตอร์	home	15	t	2024-11-11 21:34:42.989335	2024-11-11 21:34:42.989335
16	1	upload_tour	Drag 360 Image 	คลิกซ้ายค้างไว้ เพื่อเลื่อนลำดับภาพขึ้น-ลง\\n(ในกรณีลำดับภาพไม่ถูกต้อง เนื่องจากลำดับภาพจะเรียงตรงกับลำดับหมุดหรือจุดที่ถ่าย)	dashboard	16	t	2024-11-11 21:34:43.000805	2024-11-11 21:34:43.000805
17	1	upload_tour	Upload	กดเพื่ออัปโหลดภาพที่เลือกทั้งหมด 	home	17	t	2024-11-11 21:34:43.037861	2024-11-11 21:34:43.037861
18	1	upload_tour	Tour Name/Timeline Date	ตั้งชื่อTour Name /Timeline Date	home	18	t	2024-11-11 21:34:43.066796	2024-11-11 21:34:43.066796
19	1	upload_tour	Set up Pin 	คลิกจุดบนที่แปลน เพื่อปักหมุด (Pin)  ตามกับจุดที่ถ่ายรูปจริง (ตามภาพประกอบด้านล่าง)	dashboard	19	t	2024-11-11 21:34:43.079059	2024-11-11 21:34:43.079059
20	1	upload_tour	Matching image with Pin	หมายเลขภาพที่จะเชื่อมกับหมุด พร้อมภาพตัวอย่าง	dashboard	20	t	2024-11-11 21:34:43.109916	2024-11-11 21:34:43.109916
21	1	upload_tour	Save	กดบันทึกการสร้าง Tour นั้น	dashboard	21	t	2024-11-11 21:34:43.142924	2024-11-11 21:34:43.142924
22	1	project_dashboard	View Recent Tour  	ปุ่ม View สำหรับดู Tour ล่าสุดในโครงการ	dashboard	22	t	2024-11-11 21:34:43.17398	2024-11-11 21:34:43.17398
23	1	project_dashboard	Filter	Filter หรือแถบเลือกแสดงช่วงเวลาในโครงการ 	dashboard	23	t	2024-11-11 21:34:43.185429	2024-11-11 21:34:43.185429
24	1	project_dashboard	Your Team's Tasks	แสดงจำนวนงานที่ได้รับมอบหมายและที่สำเร็จแล้ว	dashboard	24	t	2024-11-11 21:34:43.225764	2024-11-11 21:34:43.225764
25	1	project_dashboard	Project Board 	แสดง Task งานที่สร้างขึ้น  โดยแบ่งเป็น 1)บอร์ด To do งานที่ยังไม่เริ่ม  2)บอร์ด Doing งานที่เริ่มและกำลังทำอยู่ และ 3)บอร์ด Done งานที่สำเร็จแล้ว	dashboard	25	t	2024-11-11 21:34:43.265194	2024-11-11 21:34:43.265194
26	1	view_tour	Select Project	เลือกโปรเจค ที่ต้องการดูทัวร์ 360	home	26	t	2024-11-11 21:34:43.283046	2024-11-11 21:34:43.283046
27	1	view_tour	View Recent Tour	ปุ่ม View สำหรับดู Tour ล่าสุดในโครงการ	dashboard	27	t	2024-11-11 21:34:43.312907	2024-11-11 21:34:43.312907
28	1	view_tour	Split Screen	คลิกที่รูปภาพค้างไว้ หมุดดูภาพทัวร์ 360 องศา	dashboard	28	t	2024-11-11 21:34:43.323539	2024-11-11 21:34:43.323539
29	1	view_tour	Pin Point	คลิกที่จุด Pin เพื่อเลือกดู 360 ทัวร์ตามแผนผัง	dashboard	29	t	2024-11-11 21:34:43.355601	2024-11-11 21:34:43.355601
30	1	view_tour	Split Screen	กดเพื่อ ดูทัวรปรียบเทียบตามช่วงเวลา	dashboard	30	t	2024-11-11 21:34:43.385227	2024-11-11 21:34:43.385227
31	1	view_tour	Select tour	เลือก ทัวร เปรียบเทียบช่วงเวลา	dashboard	31	t	2024-11-11 21:34:43.397154	2024-11-11 21:34:43.397154
32	1	view_tour	Lock view tour	เปรียบเทียบ ช่วงเวลา ในมุมมองเดียวกัน	dashboard	32	t	2024-11-11 21:34:43.428025	2024-11-11 21:34:43.428025
33	1	view_tour	Lock view tour	หมุนภาพ 2 ทัวร์ในมุมมองเดียวกัน	dashboard	33	t	2024-11-11 21:34:43.459961	2024-11-11 21:34:43.459961
34	1	comment	Comment	กล่อง Comment สามารถพิม “@”  เพื่อแจ้งผู้ร่วมงานได้	comment_guide	34	t	2024-11-11 21:34:43.471777	2024-11-11 21:34:43.471777
35	1	comment	Add Tag	เพิ่ม แท็ก ประเภทของ comment	comment_guide	35	t	2024-11-11 21:34:43.499109	2024-11-11 21:34:43.499109
36	1	comment	Add Point	หมุนหน้า View ให้จุดเล็งกลางหน้าจออยู่ในจุดที่\\nต้องการ	comment_guide	36	t	2024-11-11 21:34:43.524945	2024-11-11 21:34:43.524945
37	1	comment	Add Point	กด Add Point เพื่อให้ระบบล็อกเป้าหมายใน Comment และ Screen Shot ภาพจุดที่เล็ง	comment_guide	37	t	2024-11-11 21:34:43.535717	2024-11-11 21:34:43.535717
38	1	comment	Add Point / Remove Point	ลบเพื่อแก้ไขตำแหน่ง point	comment_guide	38	t	2024-11-11 21:34:43.545908	2024-11-11 21:34:43.545908
39	1	comment	Sent Comment	กดส่ง	comment_guide	39	t	2024-11-11 21:34:43.575218	2024-11-11 21:34:43.575218
40	1	comment	Like Comment	กด like comment	comment_guide	40	t	2024-11-11 21:34:43.589384	2024-11-11 21:34:43.589384
41	1	comment	Reply comment	แสดงความคิดเห็น ตอบกลับ 	comment_guide	41	t	2024-11-11 21:34:43.610137	2024-11-11 21:34:43.610137
42	1	comment	Comment Center	เมนูรวบรวม ความคิดเห็นต่างๆ ที่เกิดขึ้น ภายในโครงการทั้งหมด	dashboard	42	t	2024-11-11 21:34:43.638483	2024-11-11 21:34:43.638483
43	1	comment	Go to Comment 	Comment ที่เกิดขึ้นทั้งหมดในโปรเจคจะรวมอยู่ในหน้านี้ ซึ่งสามารถกดที่ Comment เพื่อไปยังจุดที่เกิด Comment	dashboard	43	t	2024-11-11 21:34:43.647572	2024-11-11 21:34:43.647572
44	1	comment	Add task 	 กดเพื่อสร้าง task ให้เชื่อมกับ Comment	dashboard	44	t	2024-11-11 21:34:43.672921	2024-11-11 21:34:43.672921
45	1	comment	View task	สำหรับกดเพื่อเปิดหน้าต่าง task โดยละเอียดในหน้า Dashboard	dashboard	45	t	2024-11-11 21:34:43.682713	2024-11-11 21:34:43.682713
46	1	file_center	File Center menu	  ศูนย์รวมทุกไฟล์ที่มีการอัฟโหลดของโปรเจค	file_center	46	t	2024-11-11 21:34:43.707147	2024-11-11 21:34:43.707147
47	1	file_center	Uploaded To	แสดงที่ๆไฟล์ถูกอัปโหลด หากอัปโหลดพร้อม\\nComment บน Tour สามารถคลิกไปดูได้เลย\\nหากอัปโหลดบน Task สามารถคลิกเพื่อไปที่ task ได้	file_center	47	t	2024-11-11 21:34:43.738027	2024-11-11 21:34:43.738027
48	1	file_center	Description	ใส่คำอธิบายของไฟล์นั้นเพิ่มเติม \\nStatus ของไฟล์ได้ เมื่อใส่แล้วจะขึ้น Status \\nและ ชื่อคนเปลี่ยน Status พร้อมกับเวลาที่เปลี่ยน Status	file_center	48	t	2024-11-11 21:34:43.749352	2024-11-11 21:34:43.749352
49	1	file_center	Status	Status ของไฟล์ได้ เมื่อใส่แล้วจะขึ้น Status \\nและ ชื่อคนเปลี่ยน Status พร้อมกับเวลาที่เปลี่ยน Status	file_center	49	t	2024-11-11 21:34:43.774369	2024-11-11 21:34:43.774369
50	1	file_center	 Tracking file	โดยที่หากไฟล์นั้น มีการเปลี่ยนแปลง จะแจ้งในผู้ที่เลือกติดตามทราบ	file_center	50	t	2024-11-11 21:34:43.805448	2024-11-11 21:34:43.805448
51	1	file_center	Add File	ปุ่ม Add file กดเพื่ออัปโหลดไฟล์ที่ File Center โดยตรง	file_center	51	t	2024-11-11 21:34:43.818467	2024-11-11 21:34:43.818467
52	1	file_center	Uploade File	กดเพื่ออัปโหลดไฟล์ที่ File Center โดยตรง	file_center	52	t	2024-11-11 21:34:43.846387	2024-11-11 21:34:43.846387
53	1	report	Report 	 รีพอร์ต (Report) สำหรับการรายงาน Comment ที่เกิดขึ้นในช่วงเวลานั้นๆ ซึ่งสามารถเก็บเป็นเอกสารตรวจสอบย้อนหลัง	report	53	t	2024-11-11 21:34:43.871179	2024-11-11 21:34:43.871179
54	1	report	Select Month , Year	เลือกเดือน, ปี ที่ต้องการทำรีพอร์ต        	report	54	t	2024-11-11 21:34:43.886965	2024-11-11 21:34:43.886965
55	1	report	Advance Setting	ตั้งค่ารีพอร์ตเพิ่มเติม            	report	55	t	2024-11-11 21:34:43.908956	2024-11-11 21:34:43.908956
56	1	report	Generate	ปุ่ม Generate เพื่อสร้างรีพอร์ตตามที่ตั้งค่า	report	56	t	2024-11-11 21:34:43.939216	2024-11-11 21:34:43.939216
57	1	invite_member	Company	เลือกที่ บริษัท	member	57	t	2024-11-11 21:34:43.950117	2024-11-11 21:34:43.950117
58	1	invite_member	Manage User	เลือก Manage Users จะเข้าสู่หน้า Manage User	member	58	t	2024-11-11 21:34:43.972948	2024-11-11 21:34:43.972948
59	1	invite_member	Role Permission	สิทธิการเข้าถึงของแต่ละบทบาท	member	59	t	2024-11-11 21:34:43.989732	2024-11-11 21:34:43.989732
60	1	invite_member	Add Admin	เพิ่ม User Company Admin	member	60	t	2024-11-11 21:34:44.00881	2024-11-11 21:34:44.00881
61	1	invite_member	Add Admin	เพิ่มสามาชิก จากรายชื่อสมาชิกเดิม	member	61	t	2024-11-11 21:34:44.019255	2024-11-11 21:34:44.019255
62	1	invite_member	Invite new member	กรณีเพิ่ม User ใหม่\\nเลือก Invite new member	member	62	t	2024-11-11 21:34:44.046702	2024-11-11 21:34:44.046702
63	1	invite_member	Invite new member	เลือกประเภทการ Invite\\n\\nBy Link  - Copy link ส่งให้สมาชิก\\nBy Email  - กรอก E-mailสมาชิก	member	63	t	2024-11-11 21:34:44.078027	2024-11-11 21:34:44.078027
64	1	invite_member	Copy link 	กด  Copy link  และส่งลิงก์ให้สมาชิก	member	64	t	2024-11-11 21:34:44.090197	2024-11-11 21:34:44.090197
65	1	invite_member	Add Member to the project \r\n(Project Admin, Collaborator\r\nAnd Observer)	เลือกแถบโครงการ  เพื่อเชิญ สมาชิกเข้าร่วมโครงการ	member	65	t	2024-11-11 21:34:44.122127	2024-11-11 21:34:44.122127
66	1	invite_member	Select the project	เลือกโครงการที่ต้องการเพิ่ม สมาชิก	member	66	t	2024-11-11 21:34:44.148721	2024-11-11 21:34:44.148721
67	1	invite_member	Add member to the project	กดเพื่อเพิ่มสมาชิกเข้าโครงการ	member	67	t	2024-11-11 21:34:44.173679	2024-11-11 21:34:44.173679
68	1	invite_member	Add  Member	เลือก บทบาท	member	68	t	2024-11-11 21:34:44.22268	2024-11-11 21:34:44.22268
69	1	invite_member	Add  Member	เพิ่มสมาชิก จากรายชื่อเดิมที่มีอยู่ ในบริษัท	member	69	t	2024-11-11 21:34:44.280832	2024-11-11 21:34:44.280832
70	1	invite_member	Invite new member	กรณีเพิ่ม User ใหม่\\nเลือก Invite new member	member	70	t	2024-11-11 21:34:44.292396	2024-11-11 21:34:44.292396
71	1	invite_member	Invite new member	เลือกประเภทการ Invite\\n\\nBy Link \\n\\n By Email 	member	71	t	2024-11-11 21:34:44.323699	2024-11-11 21:34:44.323699
72	1	invite_member	Copy link 	กด  Copy link  และส่งลิงก์ให้สมาชิก	member	72	t	2024-11-11 21:34:44.337659	2024-11-11 21:34:44.337659
\.


--
-- Data for Name: usercompletedgroups; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usercompletedgroups (user_id, step_group_name, completed_at, role_id) FROM stdin;
1	create_project	2024-11-13 01:00:32.440139	1
123	Basics	2024-11-14 01:11:34.385219	1
123	view_tour	2024-11-14 01:30:13.878976	1
\.


--
-- Data for Name: usercompletedsteps; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usercompletedsteps (user_id, step_id, completed_at, role_id) FROM stdin;
1	2	2024-11-13 01:00:32.440139	2
1	1	2024-11-13 01:00:32.440139	1
123	2	2024-11-14 01:11:34.385219	1
123	2	2024-11-14 01:30:13.878976	1
\.


--
-- Data for Name: usertutorialprogress; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usertutorialprogress (user_id, role_id, current_step_group_name, current_step_id, is_complete, created_at, updated_at) FROM stdin;
1	2	create_project	1	t	2024-11-13 01:00:32.440139	2024-11-13 01:00:32.440139
123	1	view_tour	2	t	2024-11-14 01:11:34.385219	2024-11-14 01:11:34.385219
123	1	view_tour	2	t	2024-11-14 01:30:13.878976	2024-11-14 01:30:13.878976
\.


--
-- Name: tutorialgroup_step_group_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tutorialgroup_step_group_id_seq', 10, true);


--
-- Name: tutorialstep_step_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tutorialstep_step_id_seq', 25, true);


--
-- Name: tutorialgroup tutorialgroup_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tutorialgroup
    ADD CONSTRAINT tutorialgroup_pkey PRIMARY KEY (step_group_name);


--
-- Name: tutorialstep tutorialstep_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tutorialstep
    ADD CONSTRAINT tutorialstep_pkey PRIMARY KEY (step_id);


--
-- Name: usercompletedgroups usercompletedgroups_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usercompletedgroups
    ADD CONSTRAINT usercompletedgroups_pkey PRIMARY KEY (user_id, step_group_name);


--
-- Name: tutorialgroup prevent_created_at_update_trigger; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER prevent_created_at_update_trigger BEFORE UPDATE ON public.tutorialgroup FOR EACH ROW EXECUTE FUNCTION public.set_created_at_if_null();


--
-- Name: tutorialstep tutorialstep_step_group_name_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tutorialstep
    ADD CONSTRAINT tutorialstep_step_group_name_fkey FOREIGN KEY (step_group_name) REFERENCES public.tutorialgroup(step_group_name);


--
-- PostgreSQL database dump complete
--

