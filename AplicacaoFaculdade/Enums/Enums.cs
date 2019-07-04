using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Enums {
    public enum MovimentoTipo {
        PAG,
        REC,
    }

    public enum MovimentoOrigem {
        MENSALIDADES,
        CAIXA,
        SERVICOS
    }

    public enum Order {
        ASC, 
        DESC
    }

    public enum OrderType {
        NAME,
        ID,
        DATE,
        VALUE,
    }

    public enum NivelAcessoSistema {
        ADMIN = 6,
        PROFESSOR = 7,
        FUNCIONARIO = 4,
        ALUNO = 5
    }



}