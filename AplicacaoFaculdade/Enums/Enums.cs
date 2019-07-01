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
        PAGAMENTOS,
        RECEBIMENTOS,
        SERVICOS,
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

    public enum NivelAcesso {
        SUPERADMIN = 0,
        FUNCIONARIO = 4
    }


}