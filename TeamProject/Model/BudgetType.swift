//
//  BudgetType.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 07/12/2021.
//

import Foundation

struct BudgetType: Codable, Identifiable {
    let id: Int
    let type: String
    let budget: [Budget]
}
