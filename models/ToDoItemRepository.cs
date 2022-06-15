﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Oracle 데이터 연동 - NuGet 패키지 이용
 * 
 * <현재>
 * ASP.NET CORE(1.0 -> 5.0(2019), 6.0(2022)) + view(REACT)
 * Oracle.ManagedDataAccess이 붙으려면 ASP.NET CORE 버전이 6.0이여야 함
 * 우리는 ASP.NET CORE 버전이 5.0 이기 때문에 Oracle.ManagedDataAccess에 core가 붙은 버전을 가져와야 함  
 * 
 * <예전>
 * ASP.NET 웹 | ASP.NET MVC (view -> cshtml) : Web API
 * Oracle.ManagedDataAccess 사용 가능
 */

namespace aspdotnet_react.models
{
    // CRUD 기준으로 함수 작성
    // insert, 1건 select / 여러건 select, update, delete
    /*
     * 1. SQL 구문 직접 작성해서 실행 - SQL 구문 하나 일 때
     *      전체 선택
     *      수정 (쿼리 1건 이용)
     *      삭제 (쿼리 1건 이용)
     * 2. 저장 프로시저 (PLSQL) - SQL 구문 여러 개 일 때 더 빠름
     *      등록 (쿼리 2건 이용)
     *       1. 시퀀스 아이디 얻기 (select)
     *       2. 아이디와 할일과 완료상태를 테이블에 등록 (insert)
     */
    public class TodoItemRepository
    {
        private OracleConnection conn;

        public TodoItemRepository()
        {
            conn = new OracleConnection("Data Source=xe;User Id=user1;Password=passwd;");
            conn.Open();
        }

        ~TodoItemRepository()
        {
            conn.Close();
        }

        public void createBulkTodos()
        {
            // [1] Command 객체 생성
            OracleCommand cmd = new OracleCommand();

            // [2] Connection 객체 연결
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            // [3] SQL 생성 및 실행
            for (int i = 0; i < 2500; i++)
            {
                if (i % 5 == 0)
                {
                    cmd.CommandText = $"insert into tb_todo_item (id, title, completed) values (SEQ_TODO_ITEM_ID.nextval, '리액트의 기초 ${i}', 'Y')";
                }
                else
                {
                    cmd.CommandText = $"insert into tb_todo_item (id, title, completed) values (SEQ_TODO_ITEM_ID.nextval, '리액트의 기초 ${i}', 'N')";
                }
                cmd.ExecuteNonQuery();
            }

        }
    }
}
